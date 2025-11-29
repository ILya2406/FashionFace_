using System.Threading.Tasks;

using FashionFace.Controllers.Implementations.Base;
using FashionFace.Controllers.Requests.Models;
using FashionFace.Controllers.Responses.Models;
using FashionFace.Facades.Args;
using FashionFace.Facades.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Implementations;

[Route(
    "api/v1/user/create"
)]
public sealed class UserCreateController(
    IUserCreateFacade facade
) : BaseAuthorizeController<UserCreateRequest, UserCreateResponse>
{
    [HttpPost]
    public override async Task<UserCreateResponse> Invoke(
        [FromBody] UserCreateRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserCreateArgs(
                userId,
                request.Email,
                request.Username,
                request.Password
            );

        var result =
            await
                facade
                    .Execute(
                        facadeArgs
                    );

        var response =
            new UserCreateResponse(
                result.UserId
            );

        return
            response;
    }
}