using System;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.Locations;
using FashionFace.Facades.Users.Interfaces.Locations;
using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.Locations;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Locations;

public sealed class UserLocationUpdateFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor,
    IUpdateRepository updateRepository,
    IDeleteRepository deleteRepository
) : IUserLocationUpdateFacade
{
    public async Task Execute(
        UserLocationUpdateArgs args
    )
    {
        var (
            userId,
            LocationId,
            locationType,
            cityId,
            place
            ) = args;

        var LocationCollection =
            genericReadRepository.GetCollection<Location>();

        var Location =
            await
                LocationCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.Id == LocationId
                            && entity
                                .Talent!
                                .ProfileTalent!
                                .Profile!
                                .ApplicationUserId == userId
                    );

        if (Location is null)
        {
            throw exceptionDescriptor.NotFound<Location>();
        }

        Location.CityId =
            cityId;

        var oldPlaceId =
            Location.PlaceId;

        if (locationType == LocationType.Place)
        {
            var buildingId =
                Guid.NewGuid();

            var landmarkId =
                Guid.NewGuid();

            var building =
                new Building
                {
                    Id = buildingId,
                    Name = place?.BuildingName ??  string.Empty,
                };

            var landmark =
                new Landmark
                {
                    Id = landmarkId,
                    Name = place?.LandmarkName ??  string.Empty,
                };

            Location.Place =
                new()
                {
                    Id = Guid.NewGuid(),
                    BuildingId = buildingId,
                    LandmarkId = landmarkId,
                    Street = place?.Street ?? string.Empty,
                    Building = building,
                    Landmark = landmark,

                };
        }

        await
            updateRepository
                .UpdateAsync(
                    Location
                );


        var placeCollection =
            genericReadRepository.GetCollection<Place>();

        var oldPlace =
            await
                placeCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.Id == oldPlaceId
                    );

        await
            deleteRepository
                .DeleteAsync(
                    oldPlace!
                );
    }
}