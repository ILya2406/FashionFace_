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
public sealed class UserDossierCreateController(
    IUserDossierCreateFacade facade
) : BaseUserController
{
    [HttpPost]
    public async Task Invoke(
        [FromBody] UserDossierCreateRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserDossierCreateArgs(
                userId
            );

        await
            facade
                .Execute(
                    facadeArgs
                );
    }
}