using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models;
using FashionFace.Controllers.Users.Responses.Models;
using FashionFace.Facades.Users.Args;
using FashionFace.Facades.Users.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations;

[UserControllerGroup(
    "FemaleTraits"
)]
[Route(
    "api/v1/user/female-traits"
)]
public sealed class UserFemaleTraitsController(
    IUserFemaleTraitsFacade facade
) : BaseUserController
{
    [HttpGet]
    public async Task<UserFemaleTraitsResponse> Invoke(
        [FromQuery] UserFemaleTraitsRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserFemaleTraitsArgs(
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
            new UserFemaleTraitsResponse(
                result.BustSizeType
            );

        return
            response;
    }
}