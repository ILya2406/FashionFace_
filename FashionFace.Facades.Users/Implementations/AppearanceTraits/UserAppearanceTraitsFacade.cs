using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.AppearanceTraits;
using FashionFace.Facades.Users.Interfaces.AppearanceTraits;
using FashionFace.Facades.Users.Models.AppearanceTraits;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.AppearanceTraits;

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
            genericReadRepository.GetCollection<Repositories.Context.Models.AppearanceTraits.AppearanceTraits>();

        var appearanceTraits =
            await
                appearanceTraitsCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.ProfileId == profileId
                    );

        if (appearanceTraits is null)
        {
            throw exceptionDescriptor.NotFound<Repositories.Context.Models.AppearanceTraits.AppearanceTraits>();
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