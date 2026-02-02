using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.RenderPipelines;
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
public sealed class UserRenderPipelineAttemptUpdateController(
    IUserRenderPipelineAttemptUpdateFacade facade
) : UserControllerBase
{
    [HttpPatch]
    public async Task Invoke(
        [FromBody] UserRenderPipelineAttemptUpdateRequest request
    )
    {
        var userId =
            GetUserId();

        var args =
            new UserRenderPipelineAttemptUpdateArgs(
                userId,
                request.AttemptId,
                request.UserPrompt
            );

        await
            facade
                .Execute(
                    args
                );
    }
}