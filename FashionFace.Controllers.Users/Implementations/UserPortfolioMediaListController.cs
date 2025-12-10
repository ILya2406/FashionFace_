using System.Linq;
using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Base.Responses.Models;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models;
using FashionFace.Controllers.Users.Responses.Models;
using FashionFace.Facades.Base.Models;
using FashionFace.Facades.Users.Args;
using FashionFace.Facades.Users.Interfaces;
using FashionFace.Facades.Users.Models;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations;

[UserControllerGroup(
    "Portfolio"
)]
[Route(
    "api/v1/user/portfolio/media/list"
)]
public sealed class UserPortfolioMediaListController(
    IUserPortfolioMediaListFacade facade
) : BaseUserController
{
    [HttpGet]
    public async Task<ListResponse<UserMediaListItemResponse>> Invoke(
        [FromQuery] UserPortfolioRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserPortfolioMediaListArgs(
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

    private static ListResponse<UserMediaListItemResponse> GetResponse(
        ListResult<UserMediaListItemResult> result
    )
    {
        var userMediaListItemResponseList =
            result
                .ItemList
                .Select(
                    entity =>
                        new UserMediaListItemResponse(
                            entity.Id,
                            entity.PositionIndex,
                            entity.Description,
                            entity.Url,
                            entity.TagIdList
                        )
                )
                .ToList();

        var response =
            new ListResponse<UserMediaListItemResponse>(
                result.TotalCount,
                userMediaListItemResponseList
            );

        return
            response;
    }
}