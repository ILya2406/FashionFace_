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
public sealed class UserFilterTagDeleteController(
    IUserFilterTagDeleteFacade facade
) : BaseUserController
{
    [HttpDelete]
    public async Task Invoke(
        [FromBody] UserFilterTagDeleteRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserFilterTagDeleteArgs(
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