using System.Threading.Tasks;

using FashionFace.Controllers.Anonymous.Requests.Models;
using FashionFace.Controllers.Anonymous.Responses.Models;
using FashionFace.Controllers.Base.Implementations.Base;
using FashionFace.Facades.Anonymous.Args;
using FashionFace.Facades.Anonymous.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Anonymous.Implementations.Authentication;

[Route(
    "api/v1/login"
)]
public sealed class LoginController(
    ILoginFacade facade
) : BaseAnonymousController
{
    [HttpPost]
    public async Task<LoginResponse> Invoke(
        [FromBody] LoginRequest request
    )
    {
        var facadeArgs =
            new LoginArgs(
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
            new LoginResponse(
                result.AccessToken,
                result.RefreshToken,
                result.AccessTokenExpiresAt
            );

        return
            response;
    }
}