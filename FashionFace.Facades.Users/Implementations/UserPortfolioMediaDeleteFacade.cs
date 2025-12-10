using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args;
using FashionFace.Facades.Users.Interfaces;
using FashionFace.Repositories.Context.Models;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations;

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
            portfolioMediaId
            ) = args;

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

        if (portfolioMedia is null)
        {
            throw exceptionDescriptor.NotFound<PortfolioMedia>();
        }

        await
            deleteRepository
                .DeleteAsync(
                    portfolioMedia
                );
    }
}