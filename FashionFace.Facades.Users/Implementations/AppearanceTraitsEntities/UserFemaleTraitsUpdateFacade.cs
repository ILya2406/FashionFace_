using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.AppearanceTraitsEntities;
using FashionFace.Facades.Users.Interfaces.AppearanceTraitsEntities;
using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.AppearanceTraitsEntities;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.AppearanceTraitsEntities;

public sealed class UserFemaleTraitsUpdateFacade(
    IGenericReadRepository genericReadRepository,
    IUpdateRepository updateRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserFemaleTraitsUpdateFacade
{
    public async Task Execute(
        UserFemaleTraitsUpdateArgs args
    )
    {
        var (
            userId,
            bustSizeType
            ) = args;

        var appearanceTraitsCollection =
            genericReadRepository.GetCollection<AppearanceTraits>();

        var appearanceTraits =
            await
                appearanceTraitsCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity
                                .Profile!
                                .ApplicationUserId
                            == userId
                    );

        if (appearanceTraits is null)
        {
            throw exceptionDescriptor.NotFound<AppearanceTraits>();
        }

        if (appearanceTraits.SexType == SexType.Male)
        {
            throw exceptionDescriptor.Exception(
                "InvalidSetType"
            );
        }

        var femaleTraitsCollection =
            genericReadRepository.GetCollection<FemaleTraits>();

        var femaleTraits =
            await
                femaleTraitsCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity
                                .AppearanceTraits!
                                .Profile!
                                .ApplicationUserId
                            == userId
                    );

        if (femaleTraits is null)
        {
            throw exceptionDescriptor.NotFound<FemaleTraits>();
        }

        femaleTraits.BustSizeType =
            bustSizeType;

        await
            updateRepository
                .UpdateAsync(
                    femaleTraits
                );
    }
}