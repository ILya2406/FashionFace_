using System.Threading.Tasks;

using FashionFace.Common.Models.Models;
using FashionFace.Dependencies.MassTransit.Interfaces;
using FashionFace.Facades.Users.Args.RenderPipelines;
using FashionFace.Facades.Users.Interfaces.RenderPipelines;
using FashionFace.Repositories.Context.Models.RenderPipelines;
using FashionFace.Repositories.Interfaces;

namespace FashionFace.Facades.Users.Implementations.RenderPipelines;

public sealed class UserRenderPipelineDeleteFacade(
    IBulkUpdateRepository bulkUpdateRepository,
    IEventPublishService eventPublishService
) : IUserRenderPipelineDeleteFacade
{
    public async Task Execute(
        UserRenderPipelineDeleteArgs args
    )
    {
        var (userId, pipelineId) = args;

        await
            bulkUpdateRepository
                .ExecuteUpdateAsync<RenderPipeline>(
                    entity =>
                        entity.ApplicationUserId == userId
                        && entity.Id == pipelineId,
                    entity =>
                        entity
                            .SetProperty(
                                attempt => attempt.IsDeleted,
                                true
                            )
                );

        var @event =
            new UserRenderPipelineDeletedEventModel(
                pipelineId,
                userId
            );

        await
            eventPublishService
                .PublishAsync(
                    @event
                );
    }
}