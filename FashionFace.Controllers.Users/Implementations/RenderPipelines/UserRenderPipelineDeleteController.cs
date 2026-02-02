using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.RenderPipelines;
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
public sealed class UserRenderPipelineDeleteController(
    IUserRenderPipelineDeleteFacade facade
) : UserControllerBase
{
    [HttpDelete]
    public async Task Invoke(
        [FromBody] UserRenderPipelineDeleteRequest request
    )
    {
        var userId =
            GetUserId();

        var args =
            new UserRenderPipelineDeleteArgs(
                userId,
                request.PipelineId
            );

        await
            facade
                .Execute(
                    args
                );
    }
}