using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.Filters;
using FashionFace.Controllers.Users.Responses.Models.Filters;
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
public sealed class UserFilterCreateController(
    IUserFilterCreateFacade facade
) : BaseUserController
{
    [HttpPost]
    public async Task<UserFilterCreateResponse> Invoke(
        [FromBody] UserFilterCreateRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserFilterCreateArgs(
                userId,
                request.Name
            );

        var result =
            await
                facade
                    .Execute(
                        facadeArgs
                    );

        var response =
            new UserFilterCreateResponse(
                result.FilterId
            );

        return
            response;
    }
}