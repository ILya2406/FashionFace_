using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FashionFace.Common.Constants.Constants;
using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Common.Extensions.Implementations;
using FashionFace.Common.Models.Models;
using FashionFace.Dependencies.Serialization.Interfaces;
using FashionFace.Executable.Worker.UserEvents.Args;
using FashionFace.Executable.Worker.UserEvents.Interfaces;
using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.AppearanceTraitsEntities;
using FashionFace.Repositories.Context.Models.Filters;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using RabbitMQ.Client.Events;

namespace FashionFace.Executable.Worker.UserEvents.Implementations;

public sealed class UserProfileUpdatedEventHandlerBuilder : IUserProfileUpdatedEventHandlerBuilder
{
    public AsyncEventHandler<BasicDeliverEventArgs> Build(
        EventHandlerBuilderArgs args
    ) =>
        async (
            _,
            eventArgs
        ) =>
        {
            using var scope =
                args
                    .ServiceProvider
                    .CreateScope();

            var serviceProvider =
                scope.ServiceProvider;

            var logger =
                serviceProvider.GetRequiredService<ILogger<UserProfileUpdatedEventHandlerBuilder>>();

            var serializationDecorator =
                serviceProvider.GetRequiredService<ISerializationDecorator>();

            var exceptionDescriptor =
                serviceProvider.GetRequiredService<IExceptionDescriptor>();

            var genericReadRepository =
                serviceProvider.GetRequiredService<IGenericReadRepository>();

            var createRepository =
                serviceProvider.GetRequiredService<ICreateRepository>();

            var deleteRepository =
                serviceProvider.GetRequiredService<IDeleteRepository>();

            var dictionary =
                new Dictionary<string, object>
                {
                    { "HandleId", Guid.NewGuid() },
                };

            using var loggerScope =
                logger
                    .BeginScope(
                        dictionary
                    );

            var messageAsString =
                GetMessageAsString(
                    eventArgs
                );

            var eventModel =
                serializationDecorator
                    .Deserialize<AppearanceTraitsUpdatedEventModel>(
                        messageAsString
                    );

            var profileId =
                eventModel.ProfileId;

            var appearanceTraitsCollection =
                genericReadRepository.GetCollection<AppearanceTraits>();

            var appearanceTraits =
                await
                    appearanceTraitsCollection
                        .FirstOrDefaultAsync(
                            entity => entity.ProfileId == profileId
                        );

            if (appearanceTraits is null)
            {
                throw exceptionDescriptor.NotFound<AppearanceTraits>();
            }

            var talentDimensionValueCollection =
                genericReadRepository.GetCollection<ProfileDimensionValue>();

            var talentDimensionValueList =
                await
                    talentDimensionValueCollection
                        .Include(
                            entity => entity.DimensionValue
                        )
                        .ThenInclude(
                            entity => entity.Dimension
                        )
                        .Where(
                            entity => entity.ProfileId == profileId
                        )
                        .ToListAsync();

            var dimensionTypeCode =
                TalentDimensionConstants.SexType;

            var newDimensionValueCode =
                appearanceTraits.SexType == SexType.Undefined
                    ? string.Empty
                    : appearanceTraits.SexType.ToString();

            await
                UpdateDimensionValue(
                    genericReadRepository,
                    deleteRepository,
                    createRepository,
                    exceptionDescriptor,

                    talentDimensionValueList,
                    dimensionTypeCode,
                    newDimensionValueCode,
                    profileId
                );
        };

    private static async Task UpdateDimensionValue(
        IGenericReadRepository genericReadRepository,
        IDeleteRepository deleteRepository,
        ICreateRepository createRepository,
        IExceptionDescriptor  exceptionDescriptor,

        IReadOnlyList<ProfileDimensionValue> talentDimensionValueList,
        string dimensionTypeCode,
        string newDimensionValueCode,
        Guid profileId
    )
    {
        var talentDimensionValue =
            talentDimensionValueList
                .FirstOrDefault(
                    entity =>
                        entity
                            .DimensionValue!
                            .Dimension!
                            .Code == dimensionTypeCode
                );

        var oldDimensionValueCode =
            talentDimensionValue?
                .DimensionValue?
                .Code;

        var isNotDifferent =
            newDimensionValueCode == oldDimensionValueCode;

        if (isNotDifferent)
        {
            return;
        }

        if(talentDimensionValue is not null)
        {
            await
                deleteRepository
                    .DeleteAsync(
                        talentDimensionValue
                    );
        }

        var isNotEmpty =
            newDimensionValueCode.IsNotEmpty();

        if (isNotEmpty)
        {
            var dimensionValueCollection =
                genericReadRepository.GetCollection<DimensionValue>();

            var dimensionValue =
                await
                    dimensionValueCollection
                        .FirstOrDefaultAsync(
                            entity =>
                                entity.Dimension!.Code == dimensionTypeCode
                                && entity.Code == newDimensionValueCode
                        );

            if (dimensionValue is null)
            {
                throw exceptionDescriptor.NotFound<DimensionValue>();
            }

            var newTalentDimensionValue =
                new ProfileDimensionValue
                {
                    Id = Guid.NewGuid(),
                    DimensionValueId = dimensionValue.Id,
                    ProfileId = profileId,
                };

            await
                createRepository
                    .CreateAsync(
                        newTalentDimensionValue
                    );
        }
    }

    private static string GetMessageAsString(
        BasicDeliverEventArgs basicDeliverEventArgs
    )
    {
        var body =
            basicDeliverEventArgs
                .Body
                .ToArray();

        var message =
            Encoding
                .UTF8
                .GetString(
                    body
                );

        return
            message;
    }

}