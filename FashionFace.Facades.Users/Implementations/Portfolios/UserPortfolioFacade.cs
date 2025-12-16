using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.Portfolios;
using FashionFace.Facades.Users.Interfaces.Portfolios;
using FashionFace.Facades.Users.Models.Portfolios;
using FashionFace.Repositories.Context.Models;
using FashionFace.Repositories.Context.Models.Portfolios;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Portfolios;

public sealed class UserPortfolioFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserPortfolioFacade
{
    public async Task<UserPortfolioResult> Execute(
        UserPortfolioArgs args
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
                    .FirstOrDefaultAsync(
                        entity => entity.TalentId == talentId
                    );

        if (portfolio is null)
        {
            throw exceptionDescriptor.NotFound<Portfolio>();
        }

        var result =
            new UserPortfolioResult(
                portfolio.Id,
                portfolio.Description
            );

        return
            result;
    }
}