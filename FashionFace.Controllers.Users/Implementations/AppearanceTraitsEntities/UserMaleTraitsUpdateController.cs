using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.AppearanceTraitsEntities;
using FashionFace.Facades.Users.Args.AppearanceTraitsEntities;
using FashionFace.Facades.Users.Interfaces.AppearanceTraitsEntities;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations.AppearanceTraitsEntities;

[UserControllerGroup(
    "MaleTraits"
)]
[Route(
    "api/v1/user/male-traits"
)]
public sealed class UserMaleTraitsUpdateController(
    IUserMaleTraitsUpdateFacade facade
) : BaseUserController
{
    [HttpPut]
    public async Task Invoke(
        [FromBody] UserMaleTraitsUpdateRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserMaleTraitsUpdateArgs(
                userId,
                request.FacialHairLengthType
            );

        await
            facade
                .Execute(
                    facadeArgs
                );
    }
}