using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.Profiles;
using FashionFace.Facades.Users.Args.Profiles;
using FashionFace.Facades.Users.Interfaces.Profiles;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations.Profiles;

[UserControllerGroup(
    "Profile"
)]
[Route(
    "api/v1/user/profile"
)]
public sealed class UserProfileUpdateController(
    IUserProfileUpdateFacade facade
) : UserControllerBase
{
    [HttpPut]
    public async Task Invoke(
        [FromBody] UserProfileUpdateRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserProfileUpdateArgs(
                userId,
                request.Name,
                request.City,
                request.Description,
                request.AgeCategoryType
            );

        await
            facade
                .Execute(
                    facadeArgs
                );
    }
}