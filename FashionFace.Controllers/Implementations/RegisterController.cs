using System.Threading.Tasks;

using FashionFace.Controllers.Implementations.Base;
using FashionFace.Controllers.Requests.Models;
using FashionFace.Controllers.Responses.Models;
using FashionFace.Facades.Args;
using FashionFace.Facades.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Implementations;

[Route(
    "api/v1/register"
)]
public sealed class RegisterController(
    IRegisterFacade facade
) : BaseAnonymousController<RegisterRequest, RegisterResponse>
{
    [HttpPost]
    public override async Task<RegisterResponse> Invoke(
        [FromBody] RegisterRequest request
    )
    {
        var facadeArgs =
            new RegisterArgs(
                request.Username,
                request.Password
            );

        await
            facade
                .Execute(
                    facadeArgs
                );

        return new();
    }
}