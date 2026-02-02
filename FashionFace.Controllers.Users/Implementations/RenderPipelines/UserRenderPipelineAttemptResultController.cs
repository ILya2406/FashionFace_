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
    "api/v1/user/render-pipeline/attempt/result"
)]
public sealed class UserRenderPipelineAttemptResultController(
    IUserRenderPipelineAttemptResultFacade facade
) : UserControllerBase
{
    [HttpGet]
    public async Task<UserRenderPipelineAttemptResultResponse> Invoke(
        [FromQuery] UserRenderPipelineAttemptResultRequest request
    )
    {
        var userId =
            GetUserId();

        var args =
            new UserRenderPipelineAttemptResultArgs(
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
            new UserRenderPipelineAttemptResultResponse(
                result.RelativePath
            );

        return
            response;
    }
}