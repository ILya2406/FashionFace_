using System;
using System.Text.Json;
using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.RenderPipelines;
using FashionFace.Controllers.Users.Responses.Models.RenderPipelines;
using FashionFace.Facades.Users.Args.RenderPipelines;
using FashionFace.Facades.Users.Interfaces.RenderPipelines;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FashionFace.Controllers.Users.Implementations.RenderPipelines;

[UserControllerGroup(
    "RenderPipeline"
)]
[Route(
    "api/v1/user/render-pipeline"
)]
public sealed class UserRenderPipelineCreateController(
    IUserRenderPipelineCreateFacade facade,
    ILogger<UserRenderPipelineCreateController> logger
) : UserControllerBase
{
    [HttpPost]
    public async Task<UserRenderPipelineCreateResponse> Invoke(
        [FromBody] JsonElement rawJson
    )
    {
        logger.LogInformation(
            "RAW JSON received: {RawJson}",
            rawJson.GetRawText()
        );

        var request = JsonSerializer.Deserialize<UserRenderPipelineCreateRequest>(
            rawJson.GetRawText(),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        if (request == null)
        {
            throw new Exception("Failed to deserialize request");
        }

        var userId =
            GetUserId();

        var args =
            new UserRenderPipelineCreateArgs(
                userId,
                request.TalentId,
                request.PoseReferenceId,
                request.ProductMediaAggregateId,
                request.Name
            );

        var result =
            await
                facade
                    .Execute(
                        args
                    );

        var response =
            new UserRenderPipelineCreateResponse(
                result.PipelineId
            );

        return
            response;
    }
}