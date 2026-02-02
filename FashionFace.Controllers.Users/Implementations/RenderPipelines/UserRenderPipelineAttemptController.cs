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
public sealed class UserRenderPipelineAttemptController(
    IUserRenderPipelineAttemptFacade facade
) : UserControllerBase
{
    [HttpGet]
    public async Task<UserRenderPipelineAttemptResponse> Invoke(
        [FromQuery] UserRenderPipelineAttemptRequest request
    )
    {
        var userId =
            GetUserId();

        var args =
            new UserRenderPipelineAttemptArgs(
                userId,
                request.AttemptId
            );

        var result =
            await
                facade
                    .Execute(
                        args
                    );

        var response =
            new UserRenderPipelineAttemptResponse(
                result.UserPrompt,
                result.PoseProjectionRelativePath
            );

        return
            response;
    }
}