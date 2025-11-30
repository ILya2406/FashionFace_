using System.Threading.Tasks;

using FashionFace.Controllers.Anonymous.Requests.Models.Authentication;
using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Base.Implementations.Base;
using FashionFace.Facades.Anonymous.Args;
using FashionFace.Facades.Anonymous.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Anonymous.Implementations.Authentication;

[AuthenticationControllerGroup]
[Route(
    "api/v1/authentication/register"
)]
public sealed class RegisterController(
    IRegisterFacade facade
) : BaseAnonymousController
{
    [HttpPost]
    public async Task Invoke(
        [FromBody] RegisterRequest request
    )
    {
        var facadeArgs =
            new RegisterArgs(
                request.Email,
                request.Password
            );

        await
            facade
                .Execute(
                    facadeArgs
                );
    }
}