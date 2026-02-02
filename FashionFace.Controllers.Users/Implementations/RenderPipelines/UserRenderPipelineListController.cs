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
    "RenderPipeline"
)]
[Route(
    "api/v1/user/render-pipeline/list"
)]
public sealed class UserRenderPipelineListController(
    IUserRenderPipelineListFacade facade
) : UserControllerBase
{
    [HttpGet]
    public async Task<ListResponse<UserRenderPipelineListItemResponse>> Invoke(
        [FromQuery] UserRenderPipelineListRequest request
    )
    {
        var userId =
            GetUserId();

        var args =
            new UserRenderPipelineListArgs(
                userId,
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
                        new UserRenderPipelineListItemResponse(
                            entity.Id,
                            entity.Name
                        )
                )
                .ToList();

        var response =
            new ListResponse<UserRenderPipelineListItemResponse>(
                result.TotalCount,
                itemList
            );

        return
            response;
    }
}