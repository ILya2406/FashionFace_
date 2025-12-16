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

        var LocationCollection =
            genericReadRepository.GetCollection<Location>();

        var LocationList =
            await
                LocationCollection
                    .Where(
                        entity => entity.TalentId == talentId
                    )
                    .ToListAsync();

        var LocationListItemResultList =
            new List<UserLocationListItemResult>();

        foreach (var Location in LocationCollection)
        {
            var city =
                Location.City!;

            var place =
                Location.Place;

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

            var LocationListItemResult =
                new UserLocationListItemResult(
                    Location.Id,
                    Location.LocationType,
                    cityModel,
                    placeModel
                );

            LocationListItemResultList
                .Add(
                    LocationListItemResult
                );
        }

        var result =
            new ListResult<UserLocationListItemResult>(
                LocationList.Count,
                LocationListItemResultList
            );

        return
            result;
    }
}