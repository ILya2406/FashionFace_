using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.Portfolios;
using FashionFace.Facades.Users.Interfaces.Portfolios;
using FashionFace.Repositories.Context.Models;
using FashionFace.Repositories.Context.Models.Portfolios;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Portfolios;

public sealed class UserPortfolioTagDeleteFacade(
    IGenericReadRepository genericReadRepository,
    IDeleteRepository deleteRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserPortfolioTagDeleteFacade
{
    public async Task Execute(
        UserPortfolioTagDeleteArgs args
    )
    {
        var (
            userId,
            tagId,
            portfolioId
            ) = args;

        var portfolioTagCollection =
            genericReadRepository.GetCollection<PortfolioTag>();

        var portfolioTag =
            await
                portfolioTagCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.TagId == tagId
                            && entity.PortfolioId == portfolioId
                            && entity
                                .Portfolio!
                                .Talent!
                                .ProfileTalent!
                                .Profile!
                                .ApplicationUserId
                            == userId
                    );

        if (portfolioTag is null)
        {
            throw exceptionDescriptor.NotFound<PortfolioTag>();
        }

        await
            deleteRepository
                .DeleteAsync(
                    portfolioTag
                );
    }
}