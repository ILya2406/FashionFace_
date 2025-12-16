using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.AppearanceTraits;
using FashionFace.Facades.Users.Interfaces.AppearanceTraits;
using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models;
using FashionFace.Repositories.Context.Models.AppearanceTraits;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.AppearanceTraits;

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
            genericReadRepository.GetCollection<Repositories.Context.Models.AppearanceTraits.AppearanceTraits>();

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
            throw exceptionDescriptor.NotFound<Repositories.Context.Models.AppearanceTraits.AppearanceTraits>();
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