using System;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.Locations;
using FashionFace.Facades.Users.Interfaces.Locations;
using FashionFace.Facades.Users.Models.Locations;
using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.Locations;
using FashionFace.Repositories.Context.Models.Talents;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Locations;

public sealed class UserLocationCreateFacade(
    IGenericReadRepository genericReadRepository,
    ICreateRepository createRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserLocationCreateFacade
{
    public async Task<UserLocationCreateResult> Execute(
        UserLocationCreateArgs args
    )
    {
        var (
            userId,
            talentId,
            locationType,
            cityId,
            place
            ) = args;

        var talentCollection =
            genericReadRepository.GetCollection<Talent>();

        var talent =
            await
                talentCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.Id == talentId
                            && entity
                                .ProfileTalent!
                                .Profile!
                                .ApplicationUserId
                            == userId
                    );

        if (talent is null)
        {
            throw exceptionDescriptor.NotFound<Talent>();
        }

        var isPlaceType =
            locationType == LocationType.Place;

        var buildingName =
            isPlaceType
            && place?.BuildingName is not null
                ? place.BuildingName
                : string.Empty;

        var landmarkName =
            isPlaceType
            && place?.LandmarkName is not null
                ? place.LandmarkName
                : string.Empty;

        var street =
            isPlaceType
            && place?.Street is not null
                ? place.Street
                : string.Empty;

        var buildingId =
            Guid.NewGuid();

        var landmarkId =
            Guid.NewGuid();

        var building =
            new Building
            {
                Id = buildingId,
                Name = buildingName,
            };

        var landmark =
            new Landmark
            {
                Id = landmarkId,
                Name = landmarkName,
            };

        var placeId =
            Guid.NewGuid();

        var newPlace =
            new Place
            {
                Id = placeId,
                BuildingId = buildingId,
                LandmarkId = landmarkId,
                Street = street,
                Building = building,
                Landmark = landmark,
            };

        var location =
            new Location
            {
                Id = Guid.NewGuid(),
                IsDeleted = false,
                TalentId = talentId,
                LocationType = locationType,
                CityId = cityId,
                PlaceId = placeId,
                Place = newPlace,
            };

        await
            createRepository
                .CreateAsync(
                    location
                );

        var result =
            new UserLocationCreateResult(
                location.Id
            );

        return
            result;
    }
}