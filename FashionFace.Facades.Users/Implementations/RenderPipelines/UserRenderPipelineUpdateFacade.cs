using System.Threading.Tasks;

using FashionFace.Common.Models.Models;
using FashionFace.Dependencies.MassTransit.Interfaces;
using FashionFace.Facades.Users.Args.RenderPipelines;
using FashionFace.Facades.Users.Interfaces.RenderPipelines;
using FashionFace.Repositories.Context.Models.RenderPipelines;
using FashionFace.Repositories.Interfaces;

namespace FashionFace.Facades.Users.Implementations.RenderPipelines;

public sealed class UserRenderPipelineUpdateFacade(
    IBulkUpdateRepository bulkUpdateRepository,
    IEventPublishService eventPublishService
) : IUserRenderPipelineUpdateFacade
{
    public async Task Execute(
        UserRenderPipelineUpdateArgs args
    )
    {
        var (userId, pipelineId, name, description) = args;

        if(name is not null)
        {
            await
                bulkUpdateRepository
                    .ExecuteUpdateAsync<RenderPipeline>(
                        entity =>
                            entity.ApplicationUserId == userId
                            && entity.Id == pipelineId,
                        entity =>
                            entity
                                .SetProperty(
                                    attempt =>
                                        attempt.Name,
                                    name
                                )
                    );
        }

        if(description is not null)
        {
            await
                bulkUpdateRepository
                    .ExecuteUpdateAsync<RenderPipeline>(
                        entity =>
                            entity.ApplicationUserId == userId
                            && entity.Id == pipelineId,
                        entity =>
                            entity
                                .SetProperty(
                                    attempt =>
                                        attempt.Description,
                                    description
                                )
                    );
        }

        var @event =
            new UserRenderPipelineUpdatedEventModel(
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