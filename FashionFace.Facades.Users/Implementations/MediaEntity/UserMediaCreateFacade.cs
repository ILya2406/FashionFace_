using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.MediaEntity;
using FashionFace.Facades.Users.Interfaces.MediaEntity;
using FashionFace.Facades.Users.Models.MediaEntity;
using FashionFace.Repositories.Context.Models.Files;
using FashionFace.Repositories.Context.Models.MediaEntities;
using FashionFace.Repositories.Context.Models.Profiles;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;
using FashionFace.Services.ConfigurationSettings.Interfaces;
using FashionFace.Services.Singleton.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.MediaEntity;

public sealed class UserMediaCreateFacade(
    IGenericReadRepository genericReadRepository,
    ICreateRepository createRepository,
    IExceptionDescriptor exceptionDescriptor,
    IImageKitService imageKitService,
    IImageKitSettingsFactory imageKitSettingsFactory
): IUserMediaCreateFacade
{
    public async Task<UserMediaCreateResult> Execute(
        UserMediaCreateArgs args
    )
    {
        var (
            userId,
            stream
            ) = args;

        var profileCollection =
            genericReadRepository.GetCollection<Profile>();

        var profile =
            await
                profileCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.ApplicationUserId == userId
                    );

        if (profile is null)
        {
            throw exceptionDescriptor.NotFound<Profile>();
        }

        // Copy stream to memory for ImageKit upload
        using var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        var fileBytes = memoryStream.ToArray();

        // Upload to ImageKit
        var settings = imageKitSettingsFactory.GetSettings();
        var imageUrl = await imageKitService.UploadPhotoBytes(
            fileBytes,
            $"{Guid.NewGuid()}.jpg",
            settings.Folder,
            settings.PrivateKey
        );

        if (string.IsNullOrEmpty(imageUrl))
        {
            throw exceptionDescriptor.Exception("Failed to upload image to ImageKit");
        }

        // Create Media entity with ImageKit URL
        var mediaId = Guid.NewGuid();
        var originalFileId = Guid.NewGuid();
        var optimizedFileId = Guid.NewGuid();
        var originalFileResourceId = Guid.NewGuid();
        var optimizedFileResourceId = Guid.NewGuid();

        // Compute SHA256 hash for file
        var hashBytes = SHA256.HashData(fileBytes);
        var hashString = Convert.ToHexString(hashBytes).ToLowerInvariant();

        var fileName = $"{Guid.NewGuid()}.jpg";

        // Create FileResource for original file
        var originalFileResource = new FileResource
        {
            Id = originalFileResourceId,
            RelativePath = imageUrl,
            FileName = fileName,
            SizeBytes = fileBytes.Length,
            ContentType = "image/jpeg",
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false,
            HashSha256 = hashString
        };

        // Create FileResource for optimized file
        var optimizedFileResource = new FileResource
        {
            Id = optimizedFileResourceId,
            RelativePath = imageUrl,
            FileName = fileName,
            SizeBytes = fileBytes.Length,
            ContentType = "image/jpeg",
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false,
            HashSha256 = hashString
        };

        var originalFile = new MediaFile
        {
            Id = originalFileId,
            ProfileId = profile.Id,
            FileResourceId = originalFileResourceId,
            FileResource = originalFileResource
        };

        var optimizedFile = new MediaFile
        {
            Id = optimizedFileId,
            ProfileId = profile.Id,
            FileResourceId = optimizedFileResourceId,
            FileResource = optimizedFileResource
        };

        var media = new Media
        {
            Id = mediaId,
            IsDeleted = false,
            OriginalFileId = originalFileId,
            OptimizedFileId = optimizedFileId,
            OriginalFile = originalFile,
            OptimizedFile = optimizedFile
        };

        await createRepository.CreateAsync(media);

        // Return Media ID (NOT MediaAggregate ID)
        // MediaAggregate should be created separately via /api/v1/user/media-aggregate
        var result = new UserMediaCreateResult(
            mediaId,
            imageUrl
        );

        return result;
    }
}
