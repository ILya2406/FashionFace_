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
public sealed class UserDossierMediaDeleteController(
    IUserDossierMediaDeleteFacade facade
) : BaseUserController
{
    [ApiExplorerSettings(
        IgnoreApi = true
    )]
    [HttpDelete]
    public async Task Invoke(
        [FromBody] UserDossierMediaDeleteRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserDossierMediaDeleteArgs(
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