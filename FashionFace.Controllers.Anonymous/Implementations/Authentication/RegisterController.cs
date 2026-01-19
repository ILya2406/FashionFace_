using System.Threading.Tasks;

using FashionFace.Controllers.Anonymous.Implementations.Base;
using FashionFace.Controllers.Anonymous.Requests.Models.Authentication;
using FashionFace.Controllers.Base.Attributes.Groups;
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
) : AnonymousControllerBase
{
    [HttpPost]
    public async Task Invoke(
        [FromBody] RegisterRequest request
    )
    {
        var facadeArgs =
            new RegisterArgs(
                request.Email,
                request.Password,
                request.SexType
            );

        await
            facade
                .Execute(
                    facadeArgs
                );
    }
}