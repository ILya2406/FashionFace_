using System.Threading.Tasks;

using FashionFace.Facades.Users.Args.RenderPipelines;
using FashionFace.Facades.Users.Interfaces.RenderPipelines;
using FashionFace.Facades.Users.Models.RenderPipelines;
using FashionFace.Repositories.Context.Models.RenderPipelines;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;
using FashionFace.Services.Singleton.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.RenderPipelines;

public sealed class UserRenderPipelineCreateFacade(
    IGenericReadRepository genericReadRepository,
    ICreateRepository createRepository,
    IGuidGenerator guidGenerator,
    IDateTimePicker dateTimePicker
) : IUserRenderPipelineCreateFacade
{
    public async Task<UserRenderPipelineCreateResult> Execute(
        UserRenderPipelineCreateArgs args
    )
    {
        var (userId, talentId, poseReferenceId, productMediaAggregateId, name) = args;

        var renderPipelineCollection =
            genericReadRepository.GetCollection<RenderPipeline>();

        var renderPipeline =
            await
                renderPipelineCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.ApplicationUserId == userId
                            && entity.TalentId ==  talentId
                            && entity.PoseReferenceId == poseReferenceId
                            && entity.ProductMediaAggregateId ==  productMediaAggregateId
                    );

        if (renderPipeline is not null)
        {
            var existedResult =
                new UserRenderPipelineCreateResult(
                    renderPipeline.Id
                );

            return
                existedResult;
        }

        var newRenderPipeline =
            new RenderPipeline
            {
                Id = guidGenerator.GetNew(),
                ApplicationUserId = userId,
                TalentId = talentId,
                PoseReferenceId = poseReferenceId,
                ProductMediaAggregateId = productMediaAggregateId,
                Name = name,
                IsDeleted = false,
                Description = string.Empty,
                CreatedAt = dateTimePicker.GetUtcNow(),
            };

        await
            createRepository
                .CreateAsync(
                    newRenderPipeline
                );

        var result =
            new UserRenderPipelineCreateResult(
                newRenderPipeline.Id
            );

        return
            result;
    }
}