using System.Collections.Generic;
using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Base.Responses.Models;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models;
using FashionFace.Controllers.Users.Responses.Models;
using FashionFace.Facades.Base.Models;
using FashionFace.Facades.Users.Args;
using FashionFace.Facades.Users.Interfaces;
using FashionFace.Facades.Users.Models;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations;

[UserControllerGroup(
    "TalentLocation"
)]
[Route(
    "api/v1/user/talent-location/list"
)]
public sealed class UserTalentLocationListController(
    IUserTalentLocationListFacade facade
) : BaseUserController
{
    [HttpGet]
    public async Task<ListResponse<UserTalentLocationListItemResponse>> Invoke(
        [FromQuery] UserTalentLocationListRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserTalentLocationListArgs(
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

    private static ListResponse<UserTalentLocationListItemResponse> GetResponse(
        ListResult<UserTalentLocationListItemResult> result
    )
    {
        var talentLocationListItemResponseList =
            new List<UserTalentLocationListItemResponse>();

        foreach (var talentLocation in result.ItemList)
        {
            var city =
                talentLocation.City;

            var place =
                talentLocation.Place;

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

            var talentLocationListItemResult =
                new UserTalentLocationListItemResponse(
                    talentLocation.Id,
                    talentLocation.Type,
                    cityModel,
                    placeModel
                );

            talentLocationListItemResponseList
                .Add(
                    talentLocationListItemResult
                );
        }

        var response =
            new ListResponse<UserTalentLocationListItemResponse>(
                result.TotalCount,
                talentLocationListItemResponseList
            );

        return
            response;
    }
}