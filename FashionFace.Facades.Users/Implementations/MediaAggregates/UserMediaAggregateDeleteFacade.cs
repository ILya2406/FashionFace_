using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.MediaAggregates;
using FashionFace.Facades.Users.Interfaces.MediaAggregates;
using FashionFace.Repositories.Context.Models;
using FashionFace.Repositories.Context.Models.MediaEntities;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.MediaAggregates;

public sealed class UserMediaAggregateDeleteFacade(
    IGenericReadRepository genericReadRepository,
    IUpdateRepository updateRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserMediaAggregateDeleteFacade
{
    public async Task Execute(
        UserMediaAggregateDeleteArgs args
    )
    {
        var (
            userId,
            mediaId
            ) = args;

        var mediaAggregateCollection =
            genericReadRepository.GetCollection<MediaAggregate>();

        var mediaAggregate =
            await
                mediaAggregateCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.Id == mediaId
                            && entity
                                .OriginalMedia!
                                .OriginalFile!
                                .Profile!
                                .ApplicationUserId == userId
                    );

        if (mediaAggregate is null)
        {
            throw exceptionDescriptor.NotFound<MediaAggregate>();
        }

        mediaAggregate.IsDeleted = true;

        await
            updateRepository
                .UpdateAsync(
                    mediaAggregate
                );
    }
}