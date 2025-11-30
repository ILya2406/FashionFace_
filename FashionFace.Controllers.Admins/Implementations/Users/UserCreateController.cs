using System.Threading.Tasks;

using FashionFace.Controllers.Admins.Requests.Models.Users;
using FashionFace.Controllers.Admins.Responses.Models.Users;
using FashionFace.Controllers.Base.Implementations.Base;
using FashionFace.Facades.Admins.Args;
using FashionFace.Facades.Admins.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Admins.Implementations.Users;

[Route(
    "api/v1/admin/user/create"
)]
public sealed class UserCreateController(
    IUserCreateFacade facade
) : BaseAuthorizeController
{
    [HttpPost]
    public async Task<UserCreateResponse> Invoke(
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