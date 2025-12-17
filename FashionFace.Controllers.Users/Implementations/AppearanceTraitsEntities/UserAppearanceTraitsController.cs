using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.AppearanceTraitsEntities;
using FashionFace.Controllers.Users.Responses.Models.AppearanceTraitsEntities;
using FashionFace.Facades.Users.Args.AppearanceTraitsEntities;
using FashionFace.Facades.Users.Interfaces.AppearanceTraitsEntities;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations.AppearanceTraitsEntities;

[UserControllerGroup(
    "AppearanceTraits"
)]
[Route(
    "api/v1/user/appearance-traits"
)]
public sealed class UserAppearanceTraitsController(
    IUserAppearanceTraitsFacade facade
) : BaseUserController
{
    [HttpGet]
    public async Task<UserAppearanceTraitsResponse> Invoke(
        [FromQuery] UserAppearanceTraitsRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserAppearanceTraitsArgs(
                userId,
                request.ProfileId
            );

        var result =
            await
                facade
                    .Execute(
                        facadeArgs
                    );

        var response =
            new UserAppearanceTraitsResponse(
                result.SexType,
                result.FaceType,
                result.HairColorType,
                result.HairType,
                result.HairLengthType,
                result.BodyType,
                result.SkinToneType,
                result.EyeShapeType,
                result.EyeColorType,
                result.NoseType,
                result.JawType,
                result.Height,
                result.ShoeSize
            );

        return
            response;
    }
}