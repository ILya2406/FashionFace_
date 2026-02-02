using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using FashionFace.Facades.Base.Models;
using FashionFace.Facades.Users.Args.RenderPipelines;
using FashionFace.Facades.Users.Interfaces.RenderPipelines;
using FashionFace.Facades.Users.Models.RenderPipelines;
using FashionFace.Repositories.Context.Models.RenderPipelines;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.RenderPipelines;

public sealed class UserRenderPipelineAttemptListFacade(
    IGenericReadRepository genericReadRepository
) : IUserRenderPipelineAttemptListFacade
{
    public async Task<ListResult<UserRenderPipelineAttemptListItemResult>> Execute(
        UserRenderPipelineAttemptListArgs args
    )
    {
        var (userId, pipelineId, offset, limit) = args;

        var renderPipelineAttemptCollection =
            genericReadRepository.GetCollection<RenderPipelineAttempt>();

        Expression<Func<RenderPipelineAttempt, bool>> predicate =
            entity =>
                entity.ApplicationUserId == userId
                && entity.RenderPipelineId == pipelineId;

        var totalCount =
            await
                renderPipelineAttemptCollection
                    .CountAsync(
                        predicate
                    );

        var renderPipelineAttemptList =
            await
                renderPipelineAttemptCollection
                    .Where(
                        predicate
                    )
                    .OrderByDescending(
                        entity => entity.CreatedAt
                    )
                    .Skip(
                        offset
                    )
                    .Take(
                        limit
                    )
                    .Select(
                        entity =>
                            new UserRenderPipelineAttemptListItemResult(
                                entity.Id,
                                entity.Status
                            )
                    )
                    .ToListAsync();

        var result =
            new ListResult<UserRenderPipelineAttemptListItemResult>(
                totalCount,
                renderPipelineAttemptList
            );

        return
            result;
    }
}