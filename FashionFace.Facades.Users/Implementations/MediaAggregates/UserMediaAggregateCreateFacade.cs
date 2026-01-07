using System.Threading.Tasks;

using System;
using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.MediaAggregates;
using FashionFace.Facades.Users.Interfaces.MediaAggregates;
using FashionFace.Facades.Users.Models.MediaAggregates;
using FashionFace.Repositories.Context.Models.MediaEntities;
using FashionFace.Repositories.Context.Models.Portfolios;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.MediaAggregates;

public sealed class UserMediaAggregateCreateFacade(
    IGenericReadRepository genericReadRepository,
    ICreateRepository createRepository,
    IExceptionDescriptor exceptionDescriptor
): IUserMediaAggregateCreateFacade
{
    public async Task<UserMediaAggregateCreateResult> Execute(
        UserMediaAggregateCreateArgs args
    )
    {
        var (
            userId,
            previewMediaId,
            originalMediaId,
            description
            ) = args;

        var portfolioMediaAggregateCollection =
            genericReadRepository.GetCollection<PortfolioMediaAggregate>();

        var portfolioMediaAggregate =
            await
                portfolioMediaAggregateCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            (
                                entity.MediaAggregate!.PreviewMediaId == previewMediaId
                                || entity.MediaAggregate.OriginalMediaId == originalMediaId
                                || entity.MediaAggregate.PreviewMediaId == originalMediaId
                                || entity.MediaAggregate.OriginalMediaId == previewMediaId
                            )
                            && entity
                                .Portfolio!
                                .Talent!
                                .ProfileTalent!
                                .Profile!
                                .ApplicationUserId
                            == userId
                    );

        if (portfolioMediaAggregate is not null)
        {
            throw exceptionDescriptor.Exists<PortfolioMediaAggregate>();
        }

        var mediaAggregateId =
            Guid.NewGuid();

        var mediaAggregate =
            new MediaAggregate
            {
                Id = mediaAggregateId,
                IsDeleted = false,
                Description = description,
                PreviewMediaId = previewMediaId,
                OriginalMediaId = originalMediaId,
            };

        await
            createRepository
                .CreateAsync(
                    mediaAggregate
                );

        var result =
            new UserMediaAggregateCreateResult(
                mediaAggregateId
            );

        return
            result;
    }
}