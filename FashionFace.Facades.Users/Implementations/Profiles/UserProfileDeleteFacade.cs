using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.Profiles;
using FashionFace.Facades.Users.Interfaces.Profiles;
using FashionFace.Repositories.Context.Models;
using FashionFace.Repositories.Context.Models.Profiles;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Profiles;

public sealed class UserProfileDeleteFacade(
    IGenericReadRepository genericReadRepository,
    IUpdateRepository updateRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserProfileDeleteFacade
{
    public async Task Execute(
        UserProfileDeleteArgs args
    )
    {
        var profileCollection =
            genericReadRepository.GetCollection<Profile>();

        var profile =
            await
                profileCollection
                    .FirstOrDefaultAsync(
                        entity => entity.ApplicationUserId == args.UserId
                    );

        if (profile is null)
        {
            throw exceptionDescriptor.NotFound<Profile>();
        }

        profile.IsDeleted = true;

        await
            updateRepository
                .UpdateAsync(
                    profile
                );
    }
}