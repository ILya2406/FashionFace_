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