using System.Threading.Tasks;

using FashionFace.Controllers.Implementations.Base;
using FashionFace.Controllers.Models;
using FashionFace.Facades.Args;
using FashionFace.Facades.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Implementations;

[Route(
    "api/v1/user/password/set"
)]
public sealed class UserPasswordSetController(
    IUserPasswordSetFacade facade
) : BaseAuthorizeController<UserPasswordSetRequest, UserPasswordSetResponse>
{
    [HttpPatch]
    public override async Task<UserPasswordSetResponse> Invoke(
        [FromBody] UserPasswordSetRequest request
    )
    {
        var userId =
            GetUserId();

        var userPasswordSetArgs =
            new UserPasswordSetArgs(
                userId,
                request.OldPassword,
                request.NewPassword
            );

        await
            facade
                .Execute(
                    userPasswordSetArgs
                );

        return new();
    }
}