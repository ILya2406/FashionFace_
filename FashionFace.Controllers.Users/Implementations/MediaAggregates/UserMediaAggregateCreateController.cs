using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.MediaAggregates;
using FashionFace.Controllers.Users.Responses.Models.MediaAggregates;
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
public sealed class UserMediaAggregateCreateController(
    IUserMediaAggregateCreateFacade facade
) : BaseUserController
{
    [HttpPost]
    public async Task<UserMediaAggregateCreateResponse> Invoke(
        [FromBody] UserMediaAggregateCreateRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserMediaAggregateCreateArgs(
                userId,
                request.PreviewMediaId,
                request.OriginalMediaId,
                request.Description
            );

        var result =
            await
                facade
                    .Execute(
                        facadeArgs
                    );

        var response =
            new UserMediaAggregateCreateResponse(
                result.MediaId
            );

        return
            response;
    }
}