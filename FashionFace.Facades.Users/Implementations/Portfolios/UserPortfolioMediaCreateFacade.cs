using System;
using System.Linq;
using System.Threading.Tasks;

using FashionFace.Common.Constants.Constants;
using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.Portfolios;
using FashionFace.Facades.Users.Interfaces.Portfolios;
using FashionFace.Repositories.Context.Models.Portfolios;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Portfolios;

public sealed class UserPortfolioMediaCreateFacade(
    IGenericReadRepository genericReadRepository,
    ICreateRepository createRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserPortfolioMediaCreateFacade
{
    public async Task Execute(
        UserPortfolioMediaCreateArgs args
    )
    {
        var (
            userId,
            mediaId,
            portfolioId
            ) = args;

        var portfolioCollection =
            genericReadRepository.GetCollection<Portfolio>();

        var portfolio =
            await
                portfolioCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.Id == portfolioId
                            && entity
                                .Talent!
                                .ProfileTalent!
                                .Profile!
                                .ApplicationUserId == userId
                    );

        if (portfolio is null)
        {
            throw exceptionDescriptor.NotFound<Portfolio>();
        }

        var portfolioMediaAggregateCollection =
            genericReadRepository.GetCollection<PortfolioMediaAggregate>();

        var lastPortfolioMedia =
            await
                portfolioMediaAggregateCollection
                    .Where(
                        entity =>
                            entity.PortfolioId == portfolioId
                            && entity
                                .Portfolio!
                                .Talent!
                                .ProfileTalent!
                                .Profile!
                                .ApplicationUserId
                            == userId
                    )
                    .OrderByDescending(
                        entity => entity.PositionIndex
                    )
                    .FirstOrDefaultAsync();

        var lastPositionIndex =
            lastPortfolioMedia?.PositionIndex
            ?? PositionIndexConstants.DefaultPositionIndex;

        var positionIndex =
            lastPositionIndex
            + PositionIndexConstants.PositionIndexShift;

        var newPortfolioMediaAggregate =
            new PortfolioMediaAggregate
            {
                Id = Guid.NewGuid(),
                PositionIndex = positionIndex,
                PortfolioId = portfolioId,
                MediaAggregateId = mediaId,
            };

        await
            createRepository
                .CreateAsync(
                    newPortfolioMediaAggregate
                );
    }
}