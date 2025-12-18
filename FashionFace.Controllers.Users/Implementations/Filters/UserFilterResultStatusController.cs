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
[Microsoft.AspNetCore.Components.Route(
    "api/v1/user/filter/result/status"
)]
public sealed class UserFilterResultStatusController(
    IUserFilterResultStatusFacade facade
) : BaseUserController
{
    [HttpGet]
    public async Task<UserFilterResultStatusResponse> Invoke(
        [FromQuery] UserFilterResultStatusRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserFilterResultStatusArgs(
                userId,
                request.FilterId
            );

        var result =
            await
                facade
                    .Execute(
                        facadeArgs
                    );

        var response =
            new UserFilterResultStatusResponse(
                result.Status,
                result.TalentCollectionCount
            );

        return
            response;
    }
}