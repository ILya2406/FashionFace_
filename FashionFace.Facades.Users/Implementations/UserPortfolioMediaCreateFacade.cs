using System;
using System.Linq;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args;
using FashionFace.Facades.Users.Interfaces;
using FashionFace.Repositories.Context.Models;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations;

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
            portfolioMediaId,
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

        var portfolioMediaCollection =
            genericReadRepository.GetCollection<PortfolioMedia>();

        var portfolioMedia =
            await
                portfolioMediaCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.Id == portfolioMediaId
                            && entity
                                .Portfolio!
                                .Talent!
                                .ProfileTalent!
                                .Profile!
                                .ApplicationUserId
                            == userId
                    );

        if (portfolioMedia is not null)
        {
            throw exceptionDescriptor.Exists<PortfolioMedia>();
        }

        var lastPortfolioMedia =
            await
                portfolioMediaCollection
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
            lastPortfolioMedia?.PositionIndex ?? 0;

        var positionIndex =
            lastPositionIndex + 1000;

        //todo : make something with this
        /*var newPortfolioMedia =
            new PortfolioMedia
            {
                Id = Guid.NewGuid(),
                MediaId = portfolioMediaId,
                PortfolioId = portfolioId,
                PositionIndex = positionIndex,
            };

        await
            createRepository
                .CreateAsync(
                    newPortfolioMedia
                );*/
    }
}