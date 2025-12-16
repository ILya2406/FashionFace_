using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.Profiles;
using FashionFace.Facades.Users.Interfaces.Profiles;
using FashionFace.Facades.Users.Models.Profiles;
using FashionFace.Repositories.Context.Models;
using FashionFace.Repositories.Context.Models.Profiles;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Profiles;

public sealed class UserProfileFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserProfileFacade
{
    public async Task<UserProfileResult> Execute(
        UserProfileArgs args
    )
    {
        var (
            _,
            profileId
            ) = args;

        var profileCollection =
            genericReadRepository.GetCollection<Profile>();

        var profile =
            await
                profileCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.Id == profileId
                    );

        if (profile is null)
        {
            throw exceptionDescriptor.NotFound<Profile>();
        }

        var result =
            new UserProfileResult(
                profile.Name,
                profile.Description,
                profile.AgeCategoryType,
                profile.CreatedAt
            );

        return
            result;
    }
}