using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.AppearanceTraitsEntities;
using FashionFace.Facades.Users.Args.AppearanceTraitsEntities;
using FashionFace.Facades.Users.Interfaces.AppearanceTraitsEntities;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations.AppearanceTraitsEntities;

[UserControllerGroup(
    "FemaleTraits"
)]
[Route(
    "api/v1/user/female-traits"
)]
public sealed class UserFemaleTraitsUpdateController(
    IUserFemaleTraitsUpdateFacade facade
) : BaseUserController
{
    [HttpPut]
    public async Task Invoke(
        [FromBody] UserFemaleTraitsUpdateRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserFemaleTraitsUpdateArgs(
                userId,
                request.BustSizeType
            );

        await
            facade
                .Execute(
                    facadeArgs
                );
    }
}