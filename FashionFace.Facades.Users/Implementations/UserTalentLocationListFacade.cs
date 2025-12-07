using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FashionFace.Facades.Base.Models;
using FashionFace.Facades.Users.Args;
using FashionFace.Facades.Users.Interfaces;
using FashionFace.Facades.Users.Models;
using FashionFace.Repositories.Context.Models;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations;

public sealed class UserTalentLocationListFacade(
    IGenericReadRepository genericReadRepository
) : IUserTalentLocationListFacade
{
    public async Task<ListResult<UserTalentLocationListItemResult>> Execute(
        UserTalentLocationListArgs args
    )
    {
        var (
            _,
            talentId
            ) = args;

        var talentLocationCollection =
            genericReadRepository.GetCollection<TalentLocation>();

        var talentLocationList =
            await
                talentLocationCollection
                    .Where(
                        entity => entity.TalentId == talentId
                    )
                    .ToListAsync();

        var talentLocationListItemResultList =
            new List<UserTalentLocationListItemResult>();

        foreach (var talentLocation in talentLocationCollection)
        {
            var city =
                talentLocation.City!;

            var place =
                talentLocation.Place;

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

            var talentLocationListItemResult =
                new UserTalentLocationListItemResult(
                    talentLocation.Id,
                    talentLocation.Type,
                    cityModel,
                    placeModel
                );

            talentLocationListItemResultList
                .Add(
                    talentLocationListItemResult
                );
        }

        var result =
            new ListResult<UserTalentLocationListItemResult>(
                talentLocationList.Count,
                talentLocationListItemResultList
            );

        return
            result;
    }
}