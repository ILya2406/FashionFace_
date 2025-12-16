using System.Linq;
using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Base.Responses.Models;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.Portfolios;
using FashionFace.Controllers.Users.Responses.Models.Portfolios;
using FashionFace.Facades.Base.Models;
using FashionFace.Facades.Users.Args.Portfolios;
using FashionFace.Facades.Users.Interfaces.Portfolios;
using FashionFace.Facades.Users.Models.Portfolios;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations.Portfolios;

[UserControllerGroup(
    "Portfolio"
)]
[Route(
    "api/v1/user/portfolio/tag/list"
)]
public sealed class UserPortfolioTagListController(
    IUserPortfolioTagListFacade facade
) : BaseUserController
{
    [HttpGet]
    public async Task<ListResponse<UserTagListItemResponse>> Invoke(
        [FromQuery] UserPortfolioTagListRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserPortfolioTagListArgs(
                userId,
                request.TalentId
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

    private static ListResponse<UserTagListItemResponse> GetResponse(
        ListResult<UserTagListItemResult> result
    )
    {
        var userTagListItemResponseList =
            result
                .ItemList
                .Select(
                    entity =>
                        new UserTagListItemResponse(
                            entity.Id,
                            entity.PositionIndex
                        )
                )
                .ToList();

        var response =
            new ListResponse<UserTagListItemResponse>(
                result.TotalCount,
                userTagListItemResponseList
            );

        return
            response;
    }
}