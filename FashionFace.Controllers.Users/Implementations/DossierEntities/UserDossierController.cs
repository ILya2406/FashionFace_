using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.DossierEntities;
using FashionFace.Controllers.Users.Responses.Models.DossierEntities;
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
public sealed class UserDossierController(
    IUserDossierFacade facade
) : BaseUserController
{
    [HttpGet]
    public async Task<UserDossierResponse> Invoke(
        [FromQuery] UserDossierRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserDossierArgs(
                userId
            );

        var result =
            await
                facade
                    .Execute(
                        facadeArgs
                    );

        var response =
            new UserDossierResponse(
                result.Id
            );

        return
            response;
    }
}