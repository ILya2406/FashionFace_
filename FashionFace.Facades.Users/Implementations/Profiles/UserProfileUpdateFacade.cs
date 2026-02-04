using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.Profiles;
using FashionFace.Facades.Users.Interfaces.Profiles;
using FashionFace.Repositories.Context.Models.Profiles;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Profiles;

public sealed class UserProfileUpdateFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor,
    IUpdateRepository updateRepository
) : IUserProfileUpdateFacade
{
    public async Task Execute(
        UserProfileUpdateArgs args
    )
    {
        var (
            userId,
            name,
            city,
            description,
            ageCategoryType
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

        if (name is not null)
        {
            profile.Name =
                name;
        }

        if (city is not null)
        {
            profile.City =
                city;
        }

        if (description is not null)
        {
            profile.Description =
                description;
        }

        if (ageCategoryType is not null)
        {
            profile.AgeCategoryType =
                ageCategoryType.Value;
        }

        await
            updateRepository
                .UpdateAsync(
                    profile
                );
    }
}