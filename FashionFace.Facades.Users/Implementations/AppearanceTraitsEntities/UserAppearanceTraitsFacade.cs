using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.AppearanceTraitsEntities;
using FashionFace.Facades.Users.Interfaces.AppearanceTraitsEntities;
using FashionFace.Facades.Users.Models.AppearanceTraitsEntities;
using FashionFace.Repositories.Context.Models.AppearanceTraitsEntities;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.AppearanceTraitsEntities;

public sealed class UserAppearanceTraitsFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserAppearanceTraitsFacade
{
    public async Task<UserAppearanceTraitsResult> Execute(
        UserAppearanceTraitsArgs args
    )
    {
        var (
            _,
            profileId
            ) = args;

        var appearanceTraitsCollection =
            genericReadRepository.GetCollection<AppearanceTraits>();

        var appearanceTraits =
            await
                appearanceTraitsCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.ProfileId == profileId
                    );

        if (appearanceTraits is null)
        {
            throw exceptionDescriptor.NotFound<AppearanceTraits>();
        }

        var result =
            new UserAppearanceTraitsResult(
                appearanceTraits.SexType,
                appearanceTraits.FaceType,
                appearanceTraits.HairColorType,
                appearanceTraits.HairType,
                appearanceTraits.HairLengthType,
                appearanceTraits.BodyType,
                appearanceTraits.SkinToneType,
                appearanceTraits.EyeShapeType,
                appearanceTraits.EyeColorType,
                appearanceTraits.NoseType,
                appearanceTraits.JawType,
                appearanceTraits.Height,
                appearanceTraits.ShoeSize
            );

        return
            result;
    }
}