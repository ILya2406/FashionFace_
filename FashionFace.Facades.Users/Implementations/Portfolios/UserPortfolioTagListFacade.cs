using System.Linq;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Base.Models;
using FashionFace.Facades.Users.Args.Portfolios;
using FashionFace.Facades.Users.Interfaces.Portfolios;
using FashionFace.Facades.Users.Models.Portfolios;
using FashionFace.Repositories.Context.Models.Portfolios;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Portfolios;

public sealed class UserPortfolioTagListFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserPortfolioTagListFacade
{
    public async Task<ListResult<UserTagListItemResult>> Execute(
        UserPortfolioTagListArgs args
    )
    {
        var (
            _,
            talentId
            ) = args;

        var portfolioCollection =
            genericReadRepository.GetCollection<Portfolio>();

        var portfolio =
            await
                portfolioCollection
                    .Include(
                        entity => entity.PortfolioTagCollection
                    )
                    .ThenInclude(
                        entity => entity.Tag
                    )
                    .FirstOrDefaultAsync(
                        entity => entity.TalentId == talentId
                    );

        if (portfolio is null)
        {
            throw exceptionDescriptor.NotFound<Portfolio>();
        }

        var portfolioTagCollection =
            portfolio.PortfolioTagCollection;

        var tagListResults =
            portfolioTagCollection
                .Select(
                    entity =>
                        new UserTagListItemResult(
                            entity.TagId,
                            entity.PositionIndex
                        )
                )
                .ToList();

        var result =
            new ListResult<UserTagListItemResult>(
                portfolioTagCollection.Count,
                tagListResults
            );

        return
            result;
    }
}