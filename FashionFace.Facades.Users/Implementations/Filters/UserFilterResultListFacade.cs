using System.Linq;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Base.Models;
using FashionFace.Facades.Users.Args.Filters;
using FashionFace.Facades.Users.Interfaces.Filters;
using FashionFace.Facades.Users.Models.Portfolios;
using FashionFace.Repositories.Context.Models.Filters;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Filters;

public sealed class UserFilterResultListFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserFilterResultListFacade
{
    public async Task<ListResult<UserMediaListItemResult>> Execute(
        UserFilterResultListArgs args
    )
    {
        var (
            userId,
            filterId,
            offset,
            count
            ) = args;

        var filterCollection =
            genericReadRepository.GetCollection<Filter>();

        var filter =
            await
                filterCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.Id == filterId
                            && entity.ApplicationUserId == userId
                    );

        if (filter is null)
        {
            throw exceptionDescriptor.NotFound<Filter>();
        }

        var filterResultTalentCollection =
            genericReadRepository.GetCollection<FilterResultTalent>();

        var mediaAggregateIdList =
            await
                filterResultTalentCollection
                    .Where(
                        entity =>
                            entity.IsValidated
                            && entity
                                .FilterResult!
                                .FilterId
                            == filterId
                    )
                    .OrderBy(
                        entity =>
                            entity.PositionIndex
                    )
                    .Skip(
                        offset
                    )
                    .Take(
                        count
                    )
                    .Select(
                        entity =>
                            entity
                                .Talent!
                                .TalentMediaAggregate!
                                .MediaAggregate!
                    )
                    .Select(
                        entity =>
                            new UserMediaListItemResult(

                                entity.Id,
                                offset,
                                entity.Description,
                                entity
                                    .PreviewMedia!
                                    .OptimizedFile!
                                    .RelativePath
                            )
                    )
                    .ToListAsync();

        var result =
            new ListResult<UserMediaListItemResult>(
                mediaAggregateIdList.Count,
                mediaAggregateIdList
            );

        return
            result;
    }
}