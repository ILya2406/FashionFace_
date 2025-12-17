using System;
using System.IO;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Dependencies.SkiaSharp.Interfaces;
using FashionFace.Facades.Users.Args.MediaEntity;
using FashionFace.Facades.Users.Interfaces.MediaEntity;
using FashionFace.Facades.Users.Models.MediaEntity;
using FashionFace.Repositories.Context.Models.MediaEntities;
using FashionFace.Repositories.Context.Models.Profiles;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;
using FashionFace.Services.ConfigurationSettings.Interfaces;
using FashionFace.Services.Singleton.Args;
using FashionFace.Services.Singleton.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.MediaEntity;

public sealed class UserMediaCreateFacade(
    IGenericReadRepository genericReadRepository,
    ICreateRepository createRepository,
    IExceptionDescriptor exceptionDescriptor,
    IFilePathService filePathService,
    IFileCreateService fileCreateService,
    IImageResizeService imageResizeService,
    IApplicationSettingsFactory applicationSettingsFactory
) : IUserMediaCreateFacade
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

        var mediaId =
            Guid.NewGuid();

        var originalFileId =
            Guid.NewGuid();

        var optimizedFileId =
            Guid.NewGuid();

        var originalFileRelativePath =
            filePathService
                .GetRelativePath(
                    originalFileId
                );

        var originalFile =
            new MediaFile
            {
                Id = originalFileId,
                ProfileId = profile.Id,
                RelativePath = originalFileRelativePath,
            };

        var optimizedFileRelativePath =
            filePathService
                .GetRelativePath(
                    optimizedFileId
                );

        var optimizedFile =
            new MediaFile
            {
                Id = optimizedFileId,
                ProfileId = profile.Id,
                RelativePath = optimizedFileRelativePath,
            };

        var media =
            new Media
            {
                Id = mediaId,
                IsDeleted = false,
                OriginalFileId = originalFileId,
                OptimizedFileId = optimizedFileId,
                OriginalFile = originalFile,
                OptimizedFile = optimizedFile,
            };

        await CreateFiles(
            originalFileRelativePath,
            optimizedFileRelativePath,
            stream
        );

        await
            createRepository
                .CreateAsync(
                    media
                );

        var result =
            new UserMediaCreateResult(
                mediaId
            );

        return
            result;
    }

    private async Task CreateFiles(
        string originalFileRelativePath,
        string optimizedFileRelativePath,
        Stream originalFileStream
    )
    {
        await using var optimizedFileStream =
            imageResizeService
                .Optimize(
                    originalFileStream
                );

        var applicationSettings =
            applicationSettingsFactory.GetSettings();

        var fileBasePath =
            applicationSettings.FileBasePath;

        var originalFullFilePath =
            Path
                .Combine(
                    fileBasePath,
                    originalFileRelativePath
                );

        var originalFileCreateServiceArgs =
            new FileCreateServiceArgs(
                originalFileStream,
                originalFullFilePath
            );

        await
            fileCreateService
                .Create(
                    originalFileCreateServiceArgs
                );

        var optimizedFullFilePath =
            Path
                .Combine(
                    fileBasePath,
                    optimizedFileRelativePath
                );

        var optimizedFileCreateServiceArgs =
            new FileCreateServiceArgs(
                optimizedFileStream,
                optimizedFullFilePath
            );

        await
            fileCreateService
                .Create(
                    optimizedFileCreateServiceArgs
                );
    }
}