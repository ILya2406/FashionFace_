using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FashionFace.Facades.Base.Models;
using FashionFace.Facades.Users.Args.Locations;
using FashionFace.Facades.Users.Interfaces.Locations;
using FashionFace.Facades.Users.Models.Locations;
using FashionFace.Repositories.Context.Models.Locations;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.Locations;

public sealed class UserLocationListFacade(
    IGenericReadRepository genericReadRepository
) : IUserLocationListFacade
{
    public async Task<ListResult<UserLocationListItemResult>> Execute(
        UserLocationListArgs args
    )
    {
        var (
            _,
            talentId
            ) = args;

        var locationCollection =
            genericReadRepository.GetCollection<Location>();

        var locationList =
            await
                locationCollection
                    .Where(
                        entity => entity.TalentId == talentId
                    )
                    .ToListAsync();

        var locationListItemResultList =
            new List<UserLocationListItemResult>();

        foreach (var location in locationCollection)
        {
            var city =
                location.City!;

            var place =
                location.Place;

            var cityModel =
                new CityModel(
                    city.Country,
                    city.Name
                );

            PlaceModel? placeModel = null;
            if (place is not null)
            {
                var building =
                    place.Building;

                var landmark =
                    place.Landmark;

                BuildingModel? buildingModel = null;
                LandmarkModel? landmarkModel = null;

                if (building is not null)
                {
                    buildingModel =
                        new(
                            building.Name
                        );
                }

                if (landmark is not null)
                {
                    landmarkModel =
                        new(
                            landmark.Name
                        );
                }

                placeModel =
                    new(
                        place.Street,
                        buildingModel,
                        landmarkModel
                    );
            }

            var locationListItemResult =
                new UserLocationListItemResult(
                    location.Id,
                    location.LocationType,
                    cityModel,
                    placeModel
                );

            locationListItemResultList
                .Add(
                    locationListItemResult
                );
        }

        var result =
            new ListResult<UserLocationListItemResult>(
                locationList.Count,
                locationListItemResultList
            );

        return
            result;
    }
}