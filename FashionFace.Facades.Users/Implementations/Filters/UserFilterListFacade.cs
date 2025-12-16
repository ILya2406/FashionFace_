using System.Linq;
using System.Threading.Tasks;

using FashionFace.Facades.Base.Models;
using FashionFace.Facades.Users.Args.Filters;
using FashionFace.Facades.Users.Interfaces.Filters;
using FashionFace.Facades.Users.Models.Filters;
using FashionFace.Repositories.Context.Models.Filters;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Filters;

public sealed class UserFilterListFacade(
    IGenericReadRepository genericReadRepository
) : IUserFilterListFacade
{
    public async Task<ListResult<UserFilterListItemResult>> Execute(
        UserFilterListArgs args
    )
    {
        var userId =
            args.UserId;

        var filterCollection =
            genericReadRepository.GetCollection<Filter>();

        var filterList =
            await
                filterCollection
                    .Where(
                        entity =>
                            entity.ApplicationUserId == userId
                    )
                    .Select(
                        entity =>
                            new UserFilterListItemResult(
                                entity.Id,
                                entity.PositionIndex,
                                entity.Name
                            )
                    )
                    .ToListAsync();

        var result =
            new ListResult<UserFilterListItemResult>(
                filterList.Count,
                filterList
            );

        return
            result;
    }
}