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
    "City"
)]
[Route(
    "api/v1/authorized/city/list"
)]
public sealed class AuthorizedCityListController(
    IAuthorizedCityListFacade facade
) : BaseAuthorizeController
{
    [HttpGet]
    public async Task<ListResponse<AuthorizedCityListItemResponse>> Invoke(
        [FromQuery] AuthorizedCityListRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new AuthorizedCityListArgs(
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
                        new AuthorizedCityListItemResponse(
                            entity.Id,
                            entity.Country,
                            entity.Name
                        )
                )
                .ToList();

        var response =
            new ListResponse<AuthorizedCityListItemResponse>(
                result.TotalCount,
                itemList
            );

        return
            response;
    }
}