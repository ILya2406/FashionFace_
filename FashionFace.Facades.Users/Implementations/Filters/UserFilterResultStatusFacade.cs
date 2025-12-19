using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.Filters;
using FashionFace.Facades.Users.Interfaces.Filters;
using FashionFace.Facades.Users.Models.Filters;
using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.Filters;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Filters;

public sealed class UserFilterResultStatusFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserFilterResultStatusFacade
{
    public async Task<UserFilterResultStatusResult> Execute(
        UserFilterResultStatusArgs args
    )
    {
        var (
            userId,
            filterId
            ) = args;

        var filterCollection =
            genericReadRepository.GetCollection<Filter>();

        /*var filter =
            await
                filterCollection
                    .Include(
                        entity => entity.FilterTemplate
                    )
                    .ThenInclude(
                        entity => entity.FilterResult
                    )
                    .ThenInclude(
                        entity => entity.FilterResultTalentCollection
                    )
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.Id == filterId
                            && entity.ApplicationUserId == userId
                    );

        if (filter is null)
        {
            throw exceptionDescriptor.NotFound<Filter>();
        }

        var filterFilterResult =
            filter
                .FilterTemplate!
                .FilterResult;

        if (filterFilterResult is null)
        {
            throw exceptionDescriptor.NotFound<FilterResult>();
        }

        var status =
            filterFilterResult.FilterResultStatus;

        var talentCollectionCount =
            filterFilterResult
                .FilterResultTalentCollection
                .Count;

        var result =
            new UserFilterResultStatusResult(
                status,
                talentCollectionCount
            );

        return
            result;*/

        return new UserFilterResultStatusResult(
            FilterResultStatus.Created,
            0
        );
    }
}