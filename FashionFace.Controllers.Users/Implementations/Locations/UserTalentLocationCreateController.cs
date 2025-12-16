using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.Locations;
using FashionFace.Controllers.Users.Responses.Models.Locations;
using FashionFace.Facades.Users.Args.Locations;
using FashionFace.Facades.Users.Interfaces.Locations;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations.Locations;

[UserControllerGroup(
    "Location"
)]
[Route(
    "api/v1/user/location"
)]
public sealed class UserLocationCreateController(
    IUserLocationCreateFacade facade
) : BaseUserController
{
    [HttpPost]
    public async Task<UserLocationCreateResponse> Invoke(
        [FromBody] UserLocationCreateRequest request
    )
    {
        var userId =
            GetUserId();

        var requestPlace =
            request.Place;

        var placeArgs =
            requestPlace is null
                ? null
                : new PlaceArgs(
                    requestPlace.Street,
                    requestPlace.BuildingName,
                    requestPlace.LandmarkName
                );

        var facadeArgs =
            new UserLocationCreateArgs(
                userId,
                request.TalentId,
                request.LocationType,
                request.CityId,
                placeArgs
            );

        var result =
            await
                facade
                    .Execute(
                        facadeArgs
                    );

        var response =
            new UserLocationCreateResponse(
                result.LocationId
            );

        return
            response;
    }
}