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
public sealed class UserRenderPipelineAttemptDeleteController(
    IUserRenderPipelineAttemptDeleteFacade facade
) : UserControllerBase
{
    [HttpDelete]
    public async Task Invoke(
        [FromBody] UserRenderPipelineAttemptDeleteRequest request
    )
    {
        var userId =
            GetUserId();

        var args =
            new UserRenderPipelineAttemptDeleteArgs(
                userId,
                request.AttemptId
            );

        await
            facade
                .Execute(
                    args
                );
    }
}