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
    "api/v1/user/filter/tag"
)]
public sealed class UserFilterTagCreateController(
    IUserFilterTagCreateFacade facade
) : BaseUserController
{
    [HttpPost]
    public async Task Invoke(
        [FromBody] UserFilterTagCreateRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserFilterTagCreateArgs(
                userId,
                request.FilterId,
                request.TagId
            );

        await
            facade
                .Execute(
                    facadeArgs
                );
    }
}