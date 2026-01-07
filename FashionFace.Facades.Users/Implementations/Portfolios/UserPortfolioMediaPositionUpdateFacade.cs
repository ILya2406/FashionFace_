using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.Portfolios;
using FashionFace.Facades.Users.Interfaces.Portfolios;
using FashionFace.Repositories.Context.Models.Portfolios;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Portfolios;

public sealed class UserPortfolioMediaPositionUpdateFacade(
    IGenericReadRepository genericReadRepository,
    IUpdateRepository updateRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserPortfolioMediaPositionUpdateFacade
{
    public async Task Execute(
        UserPortfolioMediaPositionUpdateArgs args
    )
    {
        var (userId, mediaId, positionIndex) = args;

        var portfolioMediaCollection =
            genericReadRepository.GetCollection<PortfolioMediaAggregate>();

        var portfolioMedia =
            await
                portfolioMediaCollection
                    .Include(entity => entity.Portfolio)
                    .ThenInclude(entity => entity!.Talent)
                    .ThenInclude(entity => entity!.ProfileTalent)
                    .ThenInclude(entity => entity!.Profile)
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.MediaAggregateId == mediaId &&
                            entity.Portfolio!.Talent!.ProfileTalent!.Profile!.ApplicationUserId == userId
                    );

        if (portfolioMedia is null)
        {
            throw exceptionDescriptor.NotFound<PortfolioMediaAggregate>();
        }

        portfolioMedia.PositionIndex = positionIndex;

        await
            updateRepository.UpdateAsync(
                portfolioMedia
            );
    }
}
