using System;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.Filters;
using FashionFace.Facades.Users.Interfaces.Filters;
using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.Filters;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Filters;

public sealed class UserFilterStartFacade(
    IGenericReadRepository genericReadRepository,
    ICreateRepository  createRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserFilterStartFacade
{
    public async Task Execute(
        UserFilterStartArgs args
    )
    {
        var (
            userId,
            filterId
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

        var filterResultCollection =
            genericReadRepository.GetCollection<FilterResult>();

        var filterResult =
            await
                filterResultCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.FilterId == filterId
                    );

        if (filterResult is not null)
        {
            throw exceptionDescriptor.Exists<FilterResult>();
        }

        var newFilterResult =
            new FilterResult
            {
                Id = Guid.NewGuid(),
                FilterId = filterId,
                FilterResultStatus = FilterResultStatus.Created,
            };

        await
            createRepository
                .CreateAsync(
                    newFilterResult
                );
    }
}