using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.AppearanceTraitsEntities;
using FashionFace.Facades.Users.Interfaces.AppearanceTraitsEntities;
using FashionFace.Facades.Users.Models.AppearanceTraitsEntities;
using FashionFace.Repositories.Context.Models.AppearanceTraitsEntities;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.AppearanceTraitsEntities;

public sealed class UserFemaleTraitsFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserFemaleTraitsFacade
{
    public async Task<UserFemaleTraitsResult> Execute(
        UserFemaleTraitsArgs args
    )
    {
        var (
            _,
            profileId
            ) = args;

        var femaleTraitsCollection =
            genericReadRepository.GetCollection<FemaleTraits>();

        var femaleTraits =
            await
                femaleTraitsCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.AppearanceTraits!.ProfileId == profileId
                    );

        if (femaleTraits is null)
        {
            throw exceptionDescriptor.NotFound<FemaleTraits>();
        }

        var result =
            new UserFemaleTraitsResult(
                femaleTraits.BustSizeType
            );

        return
            result;
    }
}