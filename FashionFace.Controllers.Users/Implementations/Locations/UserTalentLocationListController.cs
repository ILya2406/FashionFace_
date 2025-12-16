using System.Collections.Generic;
using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Base.Responses.Models;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.Locations;
using FashionFace.Controllers.Users.Responses.Models.Locations;
using FashionFace.Facades.Base.Models;
using FashionFace.Facades.Users.Args.Locations;
using FashionFace.Facades.Users.Interfaces.Locations;
using FashionFace.Facades.Users.Models.Locations;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations.Locations;

[UserControllerGroup(
    "Location"
)]
[Route(
    "api/v1/user/location/list"
)]
public sealed class UserLocationListController(
    IUserLocationListFacade facade
) : BaseUserController
{
    [HttpGet]
    public async Task<ListResponse<UserLocationListItemResponse>> Invoke(
        [FromQuery] UserLocationListRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserLocationListArgs(
                userId,
                request.TalentId
            );

        var result =
            await
                facade
                    .Execute(
                        facadeArgs
                    );

        var response =
            GetResponse(
                result
            );

        return
            response;
    }

    private static ListResponse<UserLocationListItemResponse> GetResponse(
        ListResult<UserLocationListItemResult> result
    )
    {
        var locationListItemResponseList =
            new List<UserLocationListItemResponse>();

        foreach (var location in result.ItemList)
        {
            var city =
                location.City;

            var place =
                location.Place;

            var cityModel =
                new UserCityResponse(
                    city.Country,
                    city.Name
                );

            UserPlaceResponse? placeModel = null;
            if (place is not null)
            {
                var building =
                    place.Building;

                var landmark =
                    place.Landmark;

                UserBuildingResponse? buildingModel = null;
                UserLandmarkResponse? landmarkModel = null;

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
                new UserLocationListItemResponse(
                    location.Id,
                    location.LocationType,
                    cityModel,
                    placeModel
                );

            locationListItemResponseList
                .Add(
                    locationListItemResult
                );
        }

        var response =
            new ListResponse<UserLocationListItemResponse>(
                result.TotalCount,
                locationListItemResponseList
            );

        return
            response;
    }
}