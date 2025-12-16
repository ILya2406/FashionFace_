using System.Collections.Generic;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Base.Models;
using FashionFace.Facades.Users.Args.Portfolios;
using FashionFace.Facades.Users.Interfaces.Portfolios;
using FashionFace.Facades.Users.Models.Portfolios;
using FashionFace.Repositories.Context.Models;
using FashionFace.Repositories.Context.Models.Portfolios;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Portfolios;

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
                        entity => entity.MediaAggregate
                    )
                    .ThenInclude(
                        entity => entity!.PreviewMedia
                    )
                    .ThenInclude(
                        entity => entity!.OptimizedFile
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
                    .MediaAggregate!
                    .PreviewMedia!
                    .OptimizedFile!
                    .RelativePath;

            var description =
                portfolioMedia
                    .MediaAggregate
                    .Description;

            var userMediaListItemResult =
                new UserMediaListItemResult(
                    portfolioMedia.MediaAggregateId,
                    portfolioMedia.PositionIndex,
                    description,
                    optimizedFileUri
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