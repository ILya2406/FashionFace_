using System.Threading.Tasks;

using FashionFace.Controllers.Anonymous.Requests.Models.Authentication;
using FashionFace.Controllers.Anonymous.Responses.Models.Authentication;
using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Base.Implementations.Base;
using FashionFace.Facades.Anonymous.Args;
using FashionFace.Facades.Anonymous.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Anonymous.Implementations.Authentication;

[AuthenticationControllerGroup]
[Route(
    "api/v1/authentication/login"
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