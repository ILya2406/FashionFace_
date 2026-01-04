using System.Linq;
using System.Threading.Tasks;

using FashionFace.Common.Constants.Constants;
using FashionFace.Facades.Users.Args.Filters;
using FashionFace.Facades.Users.Interfaces.Filters;
using FashionFace.Facades.Users.Models.Filters;
using FashionFace.Repositories.Context.Models.Filters;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;
using FashionFace.Services.Singleton.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Filters;

public sealed class UserFilterCreateFacade(
    IGenericReadRepository genericReadRepository,
    ICreateRepository createRepository,
    IGuidGenerator guidGenerator
) : IUserFilterCreateFacade
{
    public async Task<UserFilterCreateResult> Execute(
        UserFilterCreateArgs args
    )
    {
        var (
            userId,
            name
            ) = args;

        var filterCollection =
            genericReadRepository.GetCollection<Filter>();

        var filter =
            await
                filterCollection
                    .Where(
                        entity => entity.ApplicationUserId == userId
                    )
                    .OrderByDescending(
                        entity => entity.PositionIndex
                    )
                    .FirstOrDefaultAsync();

        var lastPositionIndex =
            filter?.PositionIndex
            ?? PositionIndexConstants.DefaultPositionIndex;

        var positionIndex =
            lastPositionIndex
            + PositionIndexConstants.PositionIndexShift;

        var filterCriteria =
            new FilterCriteria
            {
                Id = guidGenerator.GetNew(),
            };

        var newFilter =
            new Filter
            {
                Id = guidGenerator.GetNew(),
                ApplicationUserId = userId,
                IsDeleted = false,
                Name = name,
                PositionIndex = positionIndex,
                Version = IntegerVersionConstants.DefaultVersion,

                FilterCriteriaId = filterCriteria.Id,
                FilterCriteria = filterCriteria,
            };

        await
            createRepository
                .CreateAsync(
                    newFilter
                );

        var userFilterCreateResult =
            new UserFilterCreateResult(
                newFilter.Id
            );

        return
            userFilterCreateResult;
    }
}