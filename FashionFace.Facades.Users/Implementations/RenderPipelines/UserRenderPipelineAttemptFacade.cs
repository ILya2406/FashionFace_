using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.RenderPipelines;
using FashionFace.Facades.Users.Interfaces.RenderPipelines;
using FashionFace.Facades.Users.Models.RenderPipelines;
using FashionFace.Repositories.Context.Models.RenderPipelines;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.RenderPipelines;

public sealed class UserRenderPipelineAttemptFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserRenderPipelineAttemptFacade
{
    public async Task<UserRenderPipelineAttemptResult> Execute(
        UserRenderPipelineAttemptArgs args
    )
    {
        var (userId, attemptId) = args;

        var renderPipelineAttemptCollection =
            genericReadRepository.GetCollection<RenderPipelineAttempt>();

        var renderPipelineAttempt =
            await
                renderPipelineAttemptCollection
                    .Include(
                        entity => entity.RenderAttemptSettings!
                    )
                    .ThenInclude(
                        entity => entity.PoseReferenceProjection!
                    )
                    .ThenInclude(
                        entity => entity.MediaAggregate!
                    )
                    .ThenInclude(
                        entity => entity.PreviewMedia!
                    )
                    .ThenInclude(
                        entity => entity.OptimizedFile!
                    )
                    .ThenInclude(
                        entity => entity.FileResource
                    )
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.ApplicationUserId == userId
                            && entity.Id == attemptId
                    );

        if (renderPipelineAttempt is null)
        {
            throw exceptionDescriptor.NotFound<RenderPipelineAttempt>();
        }

        var renderPipelineAttemptSettings =
            renderPipelineAttempt.RenderAttemptSettings!;

        var userPrompt =
            renderPipelineAttemptSettings.UserPrompt;

        var relativePath =
            renderPipelineAttemptSettings
                .PoseReferenceProjection!
                .MediaAggregate!
                .PreviewMedia!
                .OptimizedFile!
                .FileResource!
                .RelativePath;

        var result =
            new UserRenderPipelineAttemptResult(
                renderPipelineAttempt.Id,
                userPrompt,
                relativePath
            );

        return
            result;
    }
}