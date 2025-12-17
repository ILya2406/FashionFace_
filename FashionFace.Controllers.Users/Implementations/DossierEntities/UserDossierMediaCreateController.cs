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
    "api/v1/user/dossier/media"
)]
public sealed class UserDossierMediaCreateController(
    IUserDossierMediaCreateFacade facade
) : BaseUserController
{
    [HttpPost]
    public async Task Invoke(
        [FromBody] UserDossierMediaCreateRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserDossierMediaCreateArgs(
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