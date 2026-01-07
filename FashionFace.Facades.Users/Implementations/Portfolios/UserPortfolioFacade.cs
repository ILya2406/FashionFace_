using System;
using System.Linq;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.Portfolios;
using FashionFace.Facades.Users.Interfaces.Portfolios;
using FashionFace.Facades.Users.Models.Portfolios;
using FashionFace.Repositories.Context.Models.Portfolios;
using FashionFace.Repositories.Context.Models.Profiles;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Portfolios;

public sealed class UserPortfolioFacade(
    IGenericReadRepository genericReadRepository,
    ICreateRepository createRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserPortfolioFacade
{
    public async Task<UserPortfolioResult> Execute(
        UserPortfolioArgs args
    )
    {
        var (
            userId,
            talentId
            ) = args;

        // If talentId is empty, find the first talent for the user
        if (talentId == Guid.Empty)
        {
            var profileTalentCollection =
                genericReadRepository.GetCollection<ProfileTalent>();

            var profileTalent =
                await
                    profileTalentCollection
                        .Where(entity => entity.Profile!.ApplicationUserId == userId)
                        .OrderBy(entity => entity.PositionIndex)
                        .FirstOrDefaultAsync();

            if (profileTalent is null)
            {
                throw exceptionDescriptor.NotFound<ProfileTalent>();
            }

            talentId = profileTalent.TalentId;
        }

        var portfolioCollection =
            genericReadRepository.GetCollection<Portfolio>();

        var portfolio =
            await
                portfolioCollection
                    .FirstOrDefaultAsync(
                        entity => entity.TalentId == talentId
                    );

        // If portfolio not found, create one
        if (portfolio is null)
        {
            portfolio = new Portfolio
            {
                Id = Guid.NewGuid(),
                TalentId = talentId,
                Description = string.Empty,
                IsDeleted = false
            };
            await createRepository.CreateAsync(portfolio);
        }

        var result =
            new UserPortfolioResult(
                portfolio.Id,
                talentId,
                portfolio.Description
            );

        return
            result;
    }
}