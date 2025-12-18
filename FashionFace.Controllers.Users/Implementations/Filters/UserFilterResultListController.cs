using System.Linq;
using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Base.Responses.Models;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.Filters;
using FashionFace.Controllers.Users.Responses.Models.Portfolios;
using FashionFace.Facades.Users.Args.Filters;
using FashionFace.Facades.Users.Interfaces.Filters;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations.Filters;
[UserControllerGroup(
    "Filter"
)]
[Microsoft.AspNetCore.Components.Route(
    "api/v1/user/filter/result/list"
)]
public sealed class UserFilterResultListController(
    IUserFilterResultListFacade facade
) : BaseUserController
{
    [HttpGet]
    public async Task<ListResponse<UserMediaListItemResponse>> Invoke(
        [FromQuery] UserFilterResultListRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserFilterResultListArgs(
                userId,
                request.FilterId,
                request.Offset,
                request.Count
            );

        var result =
            await
                facade
                    .Execute(
                        facadeArgs
                    );

        var userMediaListItemResponses =
            result
                .ItemList
                .Select(
                    entity =>
                        new UserMediaListItemResponse(
                            entity.Id,
                            entity.PositionIndex,
                            entity.Description,
                            entity.RelativePath
                        )
                )
                .ToList();

        var response =
            new ListResponse<UserMediaListItemResponse>(
                result.TotalCount,
                userMediaListItemResponses
            );

        return
            response;
    }
}