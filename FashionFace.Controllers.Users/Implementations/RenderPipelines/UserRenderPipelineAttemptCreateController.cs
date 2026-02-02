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
    "RenderPipelineAttempt"
)]
[Route(
    "api/v1/user/render-pipeline/attempt"
)]
public sealed class UserRenderPipelineAttemptCreateController(
    IUserRenderPipelineAttemptCreateFacade facade
) : UserControllerBase
{
    [HttpPost]
    public async Task<UserRenderPipelineAttemptCreateResponse> Invoke(
        [FromBody] UserRenderPipelineAttemptCreateRequest request
    )
    {
        var userId =
            GetUserId();

        var args =
            new UserRenderPipelineAttemptCreateArgs(
                userId,
                request.RenderPipelineId,
                request.PoseReferenceMediaAggregateId,
                request.UserPrompt,
                request.Temperature
            );

        var result =
            await
                facade
                    .Execute(
                        args
                    );

        var response =
            new UserRenderPipelineAttemptCreateResponse(
                result.AttemptId
            );

        return
            response;
    }
}