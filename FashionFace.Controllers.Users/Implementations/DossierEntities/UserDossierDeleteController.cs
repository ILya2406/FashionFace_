using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.DossierEntities;
using FashionFace.Facades.Users.Args.DossierEntities;
using FashionFace.Facades.Users.Interfaces.DossierEntities;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations.DossierEntities;

[UserControllerGroup(
    "Dossier"
)]
[Route(
    "api/v1/user/dossier"
)]
public sealed class UserDossierDeleteController(
    IUserDossierDeleteFacade facade
) : BaseUserController
{
    [HttpDelete]
    public async Task Invoke(
        [FromBody] UserDossierDeleteRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserDossierDeleteArgs(
                userId
            );

        await
            facade
                .Execute(
                    facadeArgs
                );
    }
}