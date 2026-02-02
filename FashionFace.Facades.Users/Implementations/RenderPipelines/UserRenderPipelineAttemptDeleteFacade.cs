using System.Threading.Tasks;

using FashionFace.Common.Models.Models;
using FashionFace.Dependencies.MassTransit.Interfaces;
using FashionFace.Facades.Users.Args.RenderPipelines;
using FashionFace.Facades.Users.Interfaces.RenderPipelines;
using FashionFace.Repositories.Context.Models.RenderPipelines;
using FashionFace.Repositories.Interfaces;

namespace FashionFace.Facades.Users.Implementations.RenderPipelines;

public sealed class UserRenderPipelineAttemptDeleteFacade(
    IBulkUpdateRepository bulkUpdateRepository,
    IEventPublishService eventPublishService
) : IUserRenderPipelineAttemptDeleteFacade
{
    public async Task Execute(
        UserRenderPipelineAttemptDeleteArgs args
    )
    {
        var (userId, attemptId) = args;

        await
            bulkUpdateRepository
                .ExecuteUpdateAsync<RenderPipelineAttempt>(
                    entity =>
                        entity.ApplicationUserId == userId
                        && entity.Id == attemptId,
                    entity =>
                        entity
                            .SetProperty(
                                attempt => attempt.IsDeleted,
                                true
                            )
                );

        var @event =
            new UserRenderPipelineAttemptDeletedEventModel(
                attemptId,
                userId
            );

        await
            eventPublishService
                .PublishAsync(
                    @event
                );
    }
}