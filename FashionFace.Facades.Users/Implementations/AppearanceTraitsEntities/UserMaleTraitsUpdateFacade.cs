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

public sealed class UserMaleTraitsUpdateFacade(
    IGenericReadRepository genericReadRepository,
    IUpdateRepository updateRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserMaleTraitsUpdateFacade
{
    public async Task Execute(
        UserMaleTraitsUpdateArgs args
    )
    {
        var (
            userId,
            facialHairLengthType
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

        if (appearanceTraits.SexType == SexType.Female)
        {
            throw exceptionDescriptor.Exception(
                "InvalidSetType"
            );
        }

        var maleTraitsCollection =
            genericReadRepository.GetCollection<MaleTraits>();

        var maleTraits =
            await
                maleTraitsCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity
                                .AppearanceTraits!
                                .Profile!
                                .ApplicationUserId
                            == userId
                    );

        if (maleTraits is null)
        {
            throw exceptionDescriptor.NotFound<MaleTraits>();
        }

        maleTraits.FacialHairLengthType =
            facialHairLengthType;

        await
            updateRepository
                .UpdateAsync(
                    maleTraits
                );
    }
}