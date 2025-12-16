using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.Locations;
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
public sealed class UserLocationUpdateController(
    IUserLocationUpdateFacade facade
) : BaseUserController
{
    [ApiExplorerSettings(
        IgnoreApi = true
    )]
    [HttpPut]
    public async Task Invoke(
        [FromBody] UserLocationUpdateRequest request
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
            new UserLocationUpdateArgs(
                userId,
                request.LocationId,
                request.LocationType,
                request.CityId,
                placeArgs
            );

        await
            facade
                .Execute(
                    facadeArgs
                );
    }
}