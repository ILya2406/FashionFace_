using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.AppearanceTraitsEntities;
using FashionFace.Facades.Users.Interfaces.AppearanceTraitsEntities;
using FashionFace.Repositories.Context.Models.AppearanceTraitsEntities;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.AppearanceTraitsEntities;

public sealed class UserAppearanceTraitsUpdateFacade(
    IGenericReadRepository genericReadRepository,
    IUpdateRepository updateRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserAppearanceTraitsUpdateFacade
{
    public async Task Execute(
        UserAppearanceTraitsUpdateArgs args
    )
    {
        var (
            userId,
            sexType,
            faceType,
            hairColorType,
            hairType,
            hairLengthType,
            bodyType,
            skinToneType,
            eyeShapeType,
            eyeColorType,
            noseType,
            jawType,
            height,
            shoeSize
            ) = args;

        var appearanceTraitsCollection =
            genericReadRepository.GetCollection<AppearanceTraits>();

        var appearanceTraits =
            await
                appearanceTraitsCollection
                    .Include(
                        entity => entity.MaleTraits
                    )
                    .Include(
                        entity => entity.FemaleTraits
                    )
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

        if (sexType is not null)
        {
            appearanceTraits.SexType = sexType.Value;
        }

        if (faceType is not null)
        {
            appearanceTraits.FaceType = faceType.Value;
        }

        if (hairColorType is not null)
        {
            appearanceTraits.HairColorType = hairColorType.Value;
        }

        if (hairType is not null)
        {
            appearanceTraits.HairType = hairType.Value;
        }

        if (hairLengthType is not null)
        {
            appearanceTraits.HairLengthType = hairLengthType.Value;
        }

        if (bodyType is not null)
        {
            appearanceTraits.BodyType = bodyType.Value;
        }

        if (skinToneType is not null)
        {
            appearanceTraits.SkinToneType = skinToneType.Value;
        }

        if (eyeShapeType is not null)
        {
            appearanceTraits.EyeShapeType = eyeShapeType.Value;
        }

        if (eyeColorType is not null)
        {
            appearanceTraits.EyeColorType = eyeColorType.Value;
        }

        if (noseType is not null)
        {
            appearanceTraits.NoseType = noseType.Value;
        }

        if (jawType is not null)
        {
            appearanceTraits.JawType = jawType.Value;
        }

        if (height is not null)
        {
            appearanceTraits.Height = height.Value;
        }

        if (shoeSize is not null)
        {
            appearanceTraits.ShoeSize = shoeSize.Value;
        }

        await
            updateRepository
                .UpdateAsync(
                    appearanceTraits
                );
    }
}