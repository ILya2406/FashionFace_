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
    "api/v1/user/filter/start"
)]
public sealed class UserFilterStartController(
    IUserFilterStartFacade facade
) : BaseUserController
{
    [ApiExplorerSettings(
        IgnoreApi = true
    )]
    [HttpPost]
    public async Task Invoke(
        [FromBody] UserFilterStartRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserFilterStartArgs(
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