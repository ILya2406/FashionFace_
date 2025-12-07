using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args;
using FashionFace.Facades.Users.Interfaces;
using FashionFace.Facades.Users.Models;
using FashionFace.Repositories.Context.Models;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations;

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