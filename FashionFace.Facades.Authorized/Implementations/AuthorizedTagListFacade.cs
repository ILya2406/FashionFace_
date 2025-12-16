using System.Linq;
using System.Threading.Tasks;

using FashionFace.Facades.Authorized.Args;
using FashionFace.Facades.Authorized.Interfaces;
using FashionFace.Facades.Authorized.Models;
using FashionFace.Facades.Base.Models;
using FashionFace.Repositories.Context.Models.Tags;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Authorized.Implementations;

public sealed class AuthorizedTagListFacade(
    IGenericReadRepository genericReadRepository
) : IAuthorizedTagListFacade
{
    public async Task<ListResult<AuthorizedTagListItemResult>> Execute(
        AuthorizedTagListArgs args
    )
    {
        var tagCollection =
            genericReadRepository.GetCollection<Tag>();

        var tagList =
            await
                tagCollection
                    .Select(
                        entity =>
                            new AuthorizedTagListItemResult(
                                entity.Id,
                                entity.Name
                            )
                    )
                    .ToListAsync();

        var result =
            new ListResult<AuthorizedTagListItemResult>(
                tagList.Count(),
                tagList
            );

        return
            result;
    }
}