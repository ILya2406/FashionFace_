using System.Linq;
using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.Filters;
using FashionFace.Controllers.Users.Responses.Models.AppearanceTraits;
using FashionFace.Controllers.Users.Responses.Models.Filters;
using FashionFace.Controllers.Users.Responses.Models.Locations;
using FashionFace.Controllers.Users.Responses.Models.Portfolios;
using FashionFace.Facades.Users.Args.Filters;
using FashionFace.Facades.Users.Interfaces.Filters;
using FashionFace.Facades.Users.Models.Filters;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations.Filters;

[UserControllerGroup(
    "Filter"
)]
[Route(
    "api/v1/user/filter/"
)]
public sealed class UserFilterController(
    IUserFilterFacade facade
) : BaseUserController
{
    [HttpGet]
    public async Task<UserFilterResponse> Invoke(
        [FromQuery] UserFilterRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserFilterArgs(
                userId,
                request.FilterId
            );

        var result =
            await
                facade
                    .Execute(
                        facadeArgs
                    );

        var response =
            GetUserFilterResponse(
                result
            );

        return
            response;
    }

    private static UserFilterResponse GetUserFilterResponse(
        UserFilterFacadeResult result
    )
    {
        UserFilterLocationListItemResponse? userFilterLocationListItemResponse = null;

        var userFilterLocationListItem =
            result.Location;

        if (userFilterLocationListItem is not null)
        {

            UserPlaceResponse? userPlaceResponse = null;

            var place =
                userFilterLocationListItem.Place;

            if (place is not null)
            {
                UserBuildingResponse? userBuildingResponse = null;

                var placeBuilding =
                    place.Building;

                if (placeBuilding is not null)
                {
                    userBuildingResponse =
                        new(
                            placeBuilding.Name
                        );
                }

                UserLandmarkResponse? userLandmarkResponse = null;

                var placeLandmark =
                    place.Landmark;

                if (placeLandmark is not null)
                {
                    userLandmarkResponse =
                        new(
                            placeLandmark.Name
                        );
                }

                userPlaceResponse =
                    new(
                        place.Street,
                        userBuildingResponse,
                        userLandmarkResponse
                    );
            }

            userFilterLocationListItemResponse =
                new(
                    userFilterLocationListItem.LocationType,
                    userFilterLocationListItem.CityId,
                    userPlaceResponse
                );
        }

        UserAppearanceTraitsResponse? userAppearanceTraitsResponse = null;

        var userAppearanceTraits =
            result.AppearanceTraits;

        if (userAppearanceTraits is not null)
        {
            userAppearanceTraitsResponse =
                new(
                    userAppearanceTraits.SexType,
                    userAppearanceTraits.FaceType,
                    userAppearanceTraits.HairColorType,
                    userAppearanceTraits.HairType,
                    userAppearanceTraits.HairLengthType,
                    userAppearanceTraits.BodyType,
                    userAppearanceTraits.SkinToneType,
                    userAppearanceTraits.EyeShapeType,
                    userAppearanceTraits.EyeColorType,
                    userAppearanceTraits.NoseType,
                    userAppearanceTraits.JawType,
                    userAppearanceTraits.Height,
                    userAppearanceTraits.ShoeSize
                );
        }

        var userTagListItemResponseList =
            result
                .TagList
                .Select(
                    entity =>
                        new UserTagListItemResponse(
                            entity.Id,
                            entity.PositionIndex
                        )
                )
                .ToList();

        var response =
            new UserFilterResponse(
                result.Id,
                result.Name,
                result.PositionIndex,
                result.TalentType,
                userFilterLocationListItemResponse,
                userAppearanceTraitsResponse,
                userTagListItemResponseList
            );

        return
            response;
    }
}