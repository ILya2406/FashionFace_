using System.Threading.Tasks;

using FashionFace.Controllers.Admins.Requests.Models.Users;
using FashionFace.Controllers.Base.Implementations.Base;
using FashionFace.Facades.Admins.Args;
using FashionFace.Facades.Admins.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Admins.Implementations.Users;

[Route(
    "api/v1/admin/user/password/set"
)]
public sealed class AdminUserPasswordSetController(
    IUserPasswordSetFacade facade
) : BaseAuthorizeController
{
    [HttpPatch]
    public async Task Invoke(
        [FromBody] UserPasswordSetRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserPasswordSetArgs(
                userId,
                request.OldPassword,
                request.NewPassword
            );

        await
            facade
                .Execute(
                    facadeArgs
                );
    }
}