using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.RenderPipelines;
using FashionFace.Facades.Users.Interfaces.RenderPipelines;
using FashionFace.Facades.Users.Models.RenderPipelines;
using FashionFace.Repositories.Context.Models.RenderPipelines;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.RenderPipelines;

public sealed class UserRenderPipelineFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserRenderPipelineFacade
{
    public async Task<UserRenderPipelineResult> Execute(
        UserRenderPipelineArgs args
    )
    {
        var (userId, pipelineId) = args;

        var renderPipelineCollection =
            genericReadRepository.GetCollection<RenderPipeline>();

        var renderPipeline =
            await
                renderPipelineCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.ApplicationUserId == userId
                            && entity.Id == pipelineId
                    );

        if (renderPipeline is null)
        {
            throw exceptionDescriptor.NotFound<RenderPipeline>();
        }

        var result =
            new UserRenderPipelineResult(
                renderPipeline.Id,
                renderPipeline.TalentId,
                renderPipeline.PoseReferenceId,
                renderPipeline.ProductMediaAggregateId,
                renderPipeline.Name,
                renderPipeline.Description
            );

        return
            result;
    }
}