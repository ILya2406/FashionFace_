using System.Linq;
using System;
using System.Threading.Tasks;

using FashionFace.Common.Constants.Constants;
using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.Filters;
using FashionFace.Facades.Users.Interfaces.Filters;
using FashionFace.Repositories.Context.Models.Filters;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Filters;

public sealed class UserFilterTagCreateFacade(
    IGenericReadRepository genericReadRepository,
    ICreateRepository createRepository,
    IExceptionDescriptor exceptionDescriptor
): IUserFilterTagCreateFacade
{
    public async Task Execute(
        UserFilterTagCreateArgs args
    )
    {
        var (
            userId,
            filterId,
            tagId
            ) = args;

        var filterCollection =
            genericReadRepository.GetCollection<Filter>();

        var filter =
            await
                filterCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.ApplicationUserId == userId
                            && entity.Id == filterId
                    );

        if (filter is null)
        {
            throw exceptionDescriptor.NotFound<Filter>();
        }

        var filterTagCollection =
            genericReadRepository.GetCollection<FilterCriteriaTag>();

        var filterTag =
            await
                filterTagCollection
                    .Where(
                        entity =>
                            entity.FilterCriteriaId == filter.FilterCriteriaId
                            && entity.TagId == tagId
                    )
                    .OrderByDescending(
                        entity => entity.PositionIndex
                    )
                    .FirstOrDefaultAsync();

        var lastPositionIndex =
            filterTag?.PositionIndex
            ?? PositionIndexConstants.DefaultPositionIndex;

        var positionIndex =
            lastPositionIndex
            + PositionIndexConstants.PositionIndexShift;

        var newFilterTag =
            new FilterCriteriaTag
            {
                Id = Guid.NewGuid(),
                FilterCriteriaId = filter.FilterCriteriaId,
                TagId = tagId,
                PositionIndex = positionIndex,
            };

        await
            createRepository
                .CreateAsync(
                    newFilterTag
                );
    }
}