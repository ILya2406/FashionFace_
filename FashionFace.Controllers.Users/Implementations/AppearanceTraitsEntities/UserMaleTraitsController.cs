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
    "MaleTraits"
)]
[Route(
    "api/v1/user/male-traits"
)]
public sealed class UserMaleTraitsController(
    IUserMaleTraitsFacade facade
) : BaseUserController
{
    [HttpGet]
    public async Task<UserMaleTraitsResponse> Invoke(
        [FromQuery] UserMaleTraitsRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserMaleTraitsArgs(
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
            new UserMaleTraitsResponse(
                result.FacialHairLengthType
            );

        return
            response;
    }
}