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
    "Talent"
)]
[Route(
    "api/v1/user/talent/list"
)]
public sealed class UserTalentListController(
    IUserTalentListFacade facade
) : BaseUserController
{
    [HttpGet]
    public async Task<ListResponse<UserTalentListItemResponse>> Invoke(
        [FromQuery] UserTalentListRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserTalentListArgs(
                userId,
                request.ProfileId
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

    private static ListResponse<UserTalentListItemResponse> GetResponse(
        ListResult<UserTalentListItemResult> result
    )
    {
        var talentListItemResponseList =
            result
                .ItemList
                .Select(
                    entity =>
                        new UserTalentListItemResponse(
                            entity.Id,
                            entity.Description,
                            entity.Type
                        )
                )
                .ToList();

        var response =
            new ListResponse<UserTalentListItemResponse>(
                result.TotalCount,
                talentListItemResponseList
            );

        return
            response;
    }
}