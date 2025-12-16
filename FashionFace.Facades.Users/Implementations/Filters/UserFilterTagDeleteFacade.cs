using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.Filters;
using FashionFace.Facades.Users.Interfaces.Filters;
using FashionFace.Repositories.Context.Models.Filters;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Filters;

public sealed class UserFilterTagDeleteFacade(
    IGenericReadRepository genericReadRepository,
    IDeleteRepository deleteRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserFilterTagDeleteFacade
{
    public async Task Execute(
        UserFilterTagDeleteArgs args
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
            genericReadRepository.GetCollection<FilterTag>();

        var filterTag =
            await
                filterTagCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.FilterId == filterId
                            && entity.TagId == tagId
                    );

        if (filterTag is null)
        {
            throw exceptionDescriptor.NotFound<FilterTag>();
        }

        await
            deleteRepository
                .DeleteAsync(
                    filterTag
                );
    }
}