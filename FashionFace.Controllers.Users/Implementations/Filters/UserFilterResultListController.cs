using System.Linq;
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
    "api/v1/user/filter/result/list"
)]
public sealed class UserFilterResultListController(
    IUserFilterResultListFacade facade
) : BaseUserController
{
    [HttpGet]
    public async Task<UserFilterResultListResponse> Invoke(
        [FromQuery] UserFilterResultListRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserFilterResultListArgs(
                userId,
                request.FilterId
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
                        new UserFilterResultListItemResponse(
                            entity.TalentId,
                            entity.AvatarRelativePath
                        )
                )
                .ToList();

        var response =
            new UserFilterResultListResponse(
                userMediaListItemResponses
            );

        return
            response;
    }
}