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

public sealed class UserPortfolioTagCreateFacade(
    IGenericReadRepository genericReadRepository,
    ICreateRepository createRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserPortfolioTagCreateFacade
{
    public async Task Execute(
        UserPortfolioTagCreateArgs args
    )
    {
        var (
            userId,
            tagId,
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

        var portfolioTagCollection =
            genericReadRepository.GetCollection<PortfolioTag>();

        var portfolioTag =
            await
                portfolioTagCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.TagId == tagId
                            && entity
                                .Portfolio!
                                .Talent!
                                .ProfileTalent!
                                .Profile!
                                .ApplicationUserId
                            == userId
                    );

        if (portfolioTag is not null)
        {
            throw exceptionDescriptor.Exists<PortfolioTag>();
        }

        var lastPortfolioTag =
            await
                portfolioTagCollection
                    .Where(
                        entity => entity
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
            lastPortfolioTag?.PositionIndex
            ?? PositionIndexConstants.DefaultPositionIndex;

        var positionIndex =
            lastPositionIndex
            + PositionIndexConstants.PositionIndexShift;

        var newPortfolioTag =
            new PortfolioTag
            {
                Id = Guid.NewGuid(),
                TagId = tagId,
                PortfolioId = portfolioId,
                PositionIndex = positionIndex,
            };

        await
            createRepository
                .CreateAsync(
                    newPortfolioTag
                );
    }
}