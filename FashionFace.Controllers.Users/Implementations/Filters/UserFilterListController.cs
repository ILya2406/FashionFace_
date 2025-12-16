using System.Linq;
using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Base.Responses.Models;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.Filters;
using FashionFace.Controllers.Users.Responses.Models.Filters;
using FashionFace.Facades.Base.Models;
using FashionFace.Facades.Users.Args.Filters;
using FashionFace.Facades.Users.Interfaces.Filters;
using FashionFace.Facades.Users.Models.Filters;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations.Filters;

[UserControllerGroup(
    "Filter"
)]
[Route(
    "api/v1/user/filter/list"
)]
public sealed class UserFilterListController(
    IUserFilterListFacade facade
) : BaseUserController
{
    [HttpGet]
    public async Task<ListResponse<UserFilterListItemResponse>> Invoke(
        [FromQuery] UserFilterListRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserFilterListArgs(
                userId
            );

        var result =
            await
                facade
                    .Execute(
                        facadeArgs
                    );

        var response =
            GetResponse(
                result
            );

        return
            response;
    }

    private static ListResponse<UserFilterListItemResponse> GetResponse(
        ListResult<UserFilterListItemResult> result
    )
    {
        var filterListItemResponseList =
            result
                .ItemList
                .Select(
                    entity =>
                        new UserFilterListItemResponse(
                            entity.Id,
                            entity.PositionIndex,
                            entity.Name
                        )
                )
                .ToList();

        var response =
            new ListResponse<UserFilterListItemResponse>(
                result.TotalCount,
                filterListItemResponseList
            );

        return
            response;
    }
}