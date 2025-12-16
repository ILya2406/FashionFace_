using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.Portfolios;
using FashionFace.Facades.Users.Interfaces.Portfolios;
using FashionFace.Repositories.Context.Models.Portfolios;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Portfolios;

public sealed class UserPortfolioMediaDeleteFacade(
    IGenericReadRepository genericReadRepository,
    IDeleteRepository deleteRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserPortfolioMediaDeleteFacade
{
    public async Task Execute(
        UserPortfolioMediaDeleteArgs args
    )
    {
        var (
            userId,
            mediaId
            ) = args;

        var portfolioMediaAggregateCollection =
            genericReadRepository.GetCollection<PortfolioMediaAggregate>();

        var portfolioMediaAggregate =
            await
                portfolioMediaAggregateCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.MediaAggregateId == mediaId
                            && entity
                                .Portfolio!
                                .Talent!
                                .ProfileTalent!
                                .Profile!
                                .ApplicationUserId
                            == userId
                    );

        if (portfolioMediaAggregate is null)
        {
            throw exceptionDescriptor.NotFound<PortfolioMediaAggregate>();
        }

        await
            deleteRepository
                .DeleteAsync(
                    portfolioMediaAggregate
                );
    }
}