using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.Profiles;
using FashionFace.Controllers.Users.Responses.Models.Profiles;
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
public sealed class UserProfileController(
    IUserProfileFacade facade
) : UserControllerBase
{
    [HttpGet]
    public async Task<UserProfileResponse> Invoke(
        [FromQuery] UserProfileRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserProfileArgs(
                userId,
                request.ProfileId
            );

        var result =
            await
                facade
                    .Execute(
                        facadeArgs
                    );

        var response =
            new UserProfileResponse(
                result.Id,
                result.Name,
                result.Description,
                result.AgeCategoryType,
                result.CreatedAt
            );

        return
            response;
    }
}