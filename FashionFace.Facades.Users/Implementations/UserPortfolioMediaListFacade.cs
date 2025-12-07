using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Base.Models;
using FashionFace.Facades.Users.Args;
using FashionFace.Facades.Users.Interfaces;
using FashionFace.Facades.Users.Models;
using FashionFace.Repositories.Context.Models;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations;

public sealed class UserPortfolioMediaListFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserPortfolioMediaListFacade
{
    public async Task<ListResult<UserMediaListItemResult>> Execute(
        UserPortfolioMediaListArgs args
    )
    {
        var (
            _,
            portfolioId
            ) = args;

        var portfolioCollection =
            genericReadRepository.GetCollection<Portfolio>();

        var portfolio =
            await
                portfolioCollection
                    .Include(
                        entity => entity.PortfolioMediaCollection
                    )
                    .ThenInclude(
                        entity => entity.OptimizedFile
                    )
                    .Include(
                        entity => entity.PortfolioMediaCollection
                    )
                    .ThenInclude(
                        entity => entity.PortfolioMediaTagCollection
                    )
                    .FirstOrDefaultAsync(
                        entity => entity.Id == portfolioId
                    );

        if (portfolio is null)
        {
            throw exceptionDescriptor.NotFound<Portfolio>();
        }

        var portfolioMediaCollection =
            portfolio.PortfolioMediaCollection;

        var mediaListResults =
            new List<UserMediaListItemResult>();

        foreach (var portfolioMedia in portfolioMediaCollection)
        {
            var optimizedFileUri =
                portfolioMedia
                    .OptimizedFile!
                    .Uri;

            var tagIdList =
                portfolioMedia
                    .PortfolioMediaTagCollection
                    .Select(
                        entity => entity.TagId
                    )
                    .ToList();

            var userMediaListItemResult =
                new UserMediaListItemResult(
                    portfolioMedia.Id,
                    portfolioMedia.Description,
                    optimizedFileUri,
                    tagIdList
                );

            mediaListResults
                .Add(
                    userMediaListItemResult
                );
        }

        var result =
            new ListResult<UserMediaListItemResult>(
                portfolioMediaCollection.Count,
                mediaListResults
            );

        return
            result;
    }
}