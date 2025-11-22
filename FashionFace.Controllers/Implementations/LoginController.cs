using System.Threading.Tasks;

using FashionFace.Controllers.Implementations.Base;
using FashionFace.Controllers.Models;
using FashionFace.Facades.Args;
using FashionFace.Facades.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Implementations;

[Route(
    "api/v1/login"
)]
public sealed class LoginController(
    ILoginFacade facade
) : BaseAnonymousController<LoginRequest, LoginResponse>
{
    [HttpPost]
    public override async Task<LoginResponse> Invoke(
        [FromBody] LoginRequest request
    )
    {
        var loginArgs =
            new LoginArgs(
                request.Username,
                request.Password
            );

        var result =
            await
                facade
                    .Execute(
                        loginArgs
                    );

        var response =
            new LoginResponse(
                result.AccessToken,
                result.RefreshToken,
                result.AccessTokenExpiresAt
            );

        return
            response;
    }
}