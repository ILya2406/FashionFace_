using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.Filters;
using FashionFace.Facades.Users.Args.Filters;
using FashionFace.Facades.Users.Interfaces.Filters;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations.Filters;

[UserControllerGroup(
    "Filter"
)]
[Route(
    "api/v1/user/filter"
)]
public sealed class UserFilterDeleteController(
    IUserFilterDeleteFacade facade
) : BaseUserController
{
    [HttpDelete]
    public async Task Invoke(
        [FromBody] UserFilterDeleteRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserFilterDeleteArgs(
                userId,
                request.FilterId
            );

        await
            facade
                .Execute(
                    facadeArgs
                );
    }
}