using System.Linq;
using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Base.Responses.Models;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.RenderPipelines;
using FashionFace.Controllers.Users.Responses.Models.RenderPipelines;
using FashionFace.Facades.Users.Args.RenderPipelines;
using FashionFace.Facades.Users.Interfaces.RenderPipelines;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations.RenderPipelines;

[UserControllerGroup(
    "RenderPipelineAttempt"
)]
[Route(
    "api/v1/user/render-pipeline/attempt/list"
)]
public sealed class UserRenderPipelineAttemptListController(
    IUserRenderPipelineAttemptListFacade facade
) : UserControllerBase
{
    [HttpGet]
    public async Task<ListResponse<UserRenderPipelineAttemptListItemResponse>> Invoke(
        [FromQuery] UserRenderPipelineAttemptListRequest request
    )
    {
        var userId =
            GetUserId();

        var args =
            new UserRenderPipelineAttemptListArgs(
                userId,
                request.PipelineId,
                request.Offset,
                request.Limit
            );

        var result =
            await
                facade
                    .Execute(
                        args
                    );

        var itemList =
            result
                .ItemList
                .Select(
                    entity =>
                        new UserRenderPipelineAttemptListItemResponse(
                            entity.Id
                        )
                )
                .ToList();

        var response =
            new ListResponse<UserRenderPipelineAttemptListItemResponse>(
                result.TotalCount,
                itemList
            );

        return
            response;
    }
}