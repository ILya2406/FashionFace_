using System.Linq;
using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Base.Responses.Models;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.DossierEntities;
using FashionFace.Controllers.Users.Responses.Models.Portfolios;
using FashionFace.Facades.Base.Models;
using FashionFace.Facades.Users.Args.DossierEntities;
using FashionFace.Facades.Users.Interfaces.DossierEntities;
using FashionFace.Facades.Users.Models.Portfolios;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations.DossierEntities;

[UserControllerGroup(
    "Dossier"
)]
[Route(
    "api/v1/user/dossier/media/list"
)]
public sealed class UserDossierMediaListController(
    IUserDossierMediaListFacade facade
) : BaseUserController
{
    [HttpGet]
    public async Task<ListResponse<UserMediaListItemResponse>> Invoke(
        [FromQuery] UserDossierRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserDossierMediaListArgs(
                userId
            );

        var result =
            await
                facade
                    .Execute(
                        facadeArgs
                    );

        var response =
            GetResponse(
                result
            );

        return
            response;
    }

    private static ListResponse<UserMediaListItemResponse> GetResponse(
        ListResult<UserMediaListItemResult> result
    )
    {
        var userMediaListItemResponseList =
            result
                .ItemList
                .Select(
                    entity =>
                        new UserMediaListItemResponse(
                            entity.Id,
                            entity.PositionIndex,
                            entity.Description,
                            entity.RelativePath
                        )
                )
                .ToList();

        var response =
            new ListResponse<UserMediaListItemResponse>(
                result.TotalCount,
                userMediaListItemResponseList
            );

        return
            response;
    }
}