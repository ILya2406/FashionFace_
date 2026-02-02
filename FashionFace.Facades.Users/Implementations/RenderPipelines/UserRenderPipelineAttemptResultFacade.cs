using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.RenderPipelines;
using FashionFace.Facades.Users.Interfaces.RenderPipelines;
using FashionFace.Facades.Users.Models.RenderPipelines;
using FashionFace.Repositories.Context.Models.RenderPipelines;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.RenderPipelines;

public sealed class UserRenderPipelineAttemptResultFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserRenderPipelineAttemptResultFacade
{
    public async Task<UserRenderPipelineAttemptResultResult> Execute(
        UserRenderPipelineAttemptResultArgs args
    ) {
        var (userId, attemptId) = args;

        var renderPipelineAttemptSucceededResultCollection =
            genericReadRepository.GetCollection<RenderPipelineAttemptSucceededResult>();

        var renderPipelineAttemptSucceededResult =
            await
                renderPipelineAttemptSucceededResultCollection
                    .Include(
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
                            entity
                                .RenderAttempt!
                                .ApplicationUserId
                            == userId
                            && entity
                                .RenderAttempt
                                .Id
                            == attemptId
                    );

        if (renderPipelineAttemptSucceededResult is null)
        {
            throw exceptionDescriptor.NotFound<RenderPipelineAttemptSucceededResult>();
        }

        var relativePath =
            renderPipelineAttemptSucceededResult
                .MediaAggregate!
                .PreviewMedia!
                .OptimizedFile!
                .FileResource!
                .RelativePath;

        var result =
            new UserRenderPipelineAttemptResultResult(
                relativePath
            );

        return
            result;
    }
}