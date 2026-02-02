using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
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
    "api/v1/user/render-pipeline"
)]
public sealed class UserRenderPipelineController(
    IUserRenderPipelineFacade facade
) : UserControllerBase
{
    [HttpGet]
    public async Task<UserRenderPipelineResponse> Invoke(
        [FromQuery] UserRenderPipelineRequest request
    )
    {
        var userId =
            GetUserId();

        var args =
            new UserRenderPipelineArgs(
                userId,
                request.PipelineId
            );

        var result =
            await
                facade
                    .Execute(
                        args
                    );

        var response =
            new UserRenderPipelineResponse(
                result.Id,
                result.TalentId,
                result.PoseReferenceId,
                result.ProductMediaAggregateId,
                result.Name,
                result.Description
            );

        return
            response;
    }
}