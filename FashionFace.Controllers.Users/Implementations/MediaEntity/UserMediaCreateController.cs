using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.MediaEntity;
using FashionFace.Controllers.Users.Responses.Models.MediaEntity;
using FashionFace.Facades.Users.Args.MediaEntity;
using FashionFace.Facades.Users.Interfaces.MediaEntity;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations.MediaEntity;

[UserControllerGroup(
    "Media"
)]
[Route(
    "api/v1/user/media"
)]
public sealed class UserMediaCreateController(
    IUserMediaCreateFacade facade
) : BaseUserController
{
    [HttpPost]
    public async Task<UserMediaCreateResponse> Invoke(
        [FromForm] UserMediaCreateRequest request
    )
    {
        var userId =
            GetUserId();

        await using var fileStream =
            request
                .File
                .OpenReadStream();

        var facadeArgs =
            new UserMediaCreateArgs(
                userId,
                fileStream
            );

        var result =
            await
                facade
                    .Execute(
                        facadeArgs
                    );

        var response =
            new UserMediaCreateResponse(
                result.MediaId
            );

        return
            response;
    }
}