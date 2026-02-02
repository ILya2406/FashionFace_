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
public sealed class UserRenderPipelineUpdateController(
    IUserRenderPipelineUpdateFacade facade
) : UserControllerBase
{
    [HttpPatch]
    public async Task Invoke(
        [FromBody] UserRenderPipelineUpdateRequest request
    )
    {
        var userId =
            GetUserId();

        var args =
            new UserRenderPipelineUpdateArgs(
                userId,
                request.PipelineId,
                request.Name,
                request.Description
            );

        await
            facade
                .Execute(
                    args
                );
    }
}