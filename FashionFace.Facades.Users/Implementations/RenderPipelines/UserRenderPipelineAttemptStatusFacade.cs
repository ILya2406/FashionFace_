using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.RenderPipelines;
using FashionFace.Facades.Users.Interfaces.RenderPipelines;
using FashionFace.Facades.Users.Models.RenderPipelines;
using FashionFace.Repositories.Context.Models.RenderPipelines;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.RenderPipelines;

public sealed class UserRenderPipelineAttemptStatusFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserRenderPipelineAttemptStatusFacade
{
    public async Task<UserRenderPipelineAttemptStatusResult> Execute(
        UserRenderPipelineAttemptStatusArgs args
    )
    {
        var (userId, attemptId) = args;

        var renderPipelineAttemptCollection =
            genericReadRepository.GetCollection<RenderPipelineAttempt>();

        var renderPipelineAttempt =
            await
                renderPipelineAttemptCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.ApplicationUserId == userId
                            && entity.Id == attemptId
                    );

        if (renderPipelineAttempt is null)
        {
            throw exceptionDescriptor.NotFound<RenderPipelineAttempt>();
        }

        var result =
            new UserRenderPipelineAttemptStatusResult(
                renderPipelineAttempt.Status
            );

        return
            result;
    }
}