using System.Linq;
using System.Threading.Tasks;

using FashionFace.Controllers.Authorized.Requests.Models;
using FashionFace.Controllers.Authorized.Responses.Models;
using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Base.Implementations.Base;
using FashionFace.Controllers.Base.Responses.Models;
using FashionFace.Facades.Authorized.Args;
using FashionFace.Facades.Authorized.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Authorized.Implementations;

[AdminControllerGroup(
    "Tag"
)]
[Route(
    "api/v1/authorized/tag/list"
)]
public sealed class AuthorizedTagListController(
    IAuthorizedTagListFacade facade
) : BaseAuthorizeController
{
    [HttpGet]
    public async Task<ListResponse<AuthorizedTagListItemResponse>> Invoke(
        [FromQuery] AuthorizedTagListRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new AuthorizedTagListArgs(
                userId
            );

        var result =
            await
                facade
                    .Execute(
                        facadeArgs
                    );

        var itemList =
            result
                .ItemList
                .Select(
                    entity =>
                        new AuthorizedTagListItemResponse(
                            entity.Id,
                            entity.Name
                        )
                )
                .ToList();

        var response =
            new ListResponse<AuthorizedTagListItemResponse>(
                result.TotalCount,
                itemList
            );

        return
            response;
    }
}