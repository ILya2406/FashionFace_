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

public sealed class UserRenderPipelineListFacade(
    IGenericReadRepository genericReadRepository
) : IUserRenderPipelineListFacade
{
    public async Task<ListResult<UserRenderPipelineListItemResult>> Execute(
        UserRenderPipelineListArgs args
    )
    {
        var (userId, offset, limit) = args;

        var renderPipelineCollection =
            genericReadRepository.GetCollection<RenderPipeline>();

        Expression<Func<RenderPipeline, bool>> predicate =
            entity => entity.ApplicationUserId == userId;

        var totalCount =
            await
                renderPipelineCollection
                    .CountAsync(
                        predicate
                    );

        var renderPipelineList =
            await
                renderPipelineCollection
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
                            new UserRenderPipelineListItemResult(
                                entity.Id,
                                entity.Name
                            )
                    )
                    .ToListAsync();

        var result =
            new ListResult<UserRenderPipelineListItemResult>(
                totalCount,
                renderPipelineList
            );

        return
            result;
    }
}