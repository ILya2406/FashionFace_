using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.MediaAggregates;
using FashionFace.Facades.Users.Args.MediaAggregates;
using FashionFace.Facades.Users.Interfaces.MediaAggregates;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations.MediaAggregates;

[UserControllerGroup(
    "Media"
)]
[Route(
    "api/v1/user/media-aggregate"
)]
public sealed class UserMediaAggregateDeleteController(
    IUserMediaAggregateDeleteFacade facade
) : BaseUserController
{
    [HttpDelete]
    public async Task Invoke(
        [FromBody] UserMediaAggregateDeleteRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserMediaAggregateDeleteArgs(
                userId,
                request.MediaId
            );

        await
            facade
                .Execute(
                    facadeArgs
                );
    }
}