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
public sealed class UserLocationDeleteController(
    IUserLocationDeleteFacade facade
) : BaseUserController
{
    [HttpDelete]
    public async Task Invoke(
        [FromBody] UserLocationDeleteRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserLocationDeleteArgs(
                userId,
                request.LocationId
            );

        await
            facade
                .Execute(
                    facadeArgs
                );
    }
}