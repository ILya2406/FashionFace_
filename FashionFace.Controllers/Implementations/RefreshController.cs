using System.Threading.Tasks;

using FashionFace.Controllers.Implementations.Base;
using FashionFace.Controllers.Requests.Models;
using FashionFace.Controllers.Responses.Models;
using FashionFace.Facades.Args;
using FashionFace.Facades.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Implementations;

[Route(
    "api/v1/refresh"
)]
public sealed class RefreshController(
    IRefreshFacade facade
) : BaseAnonymousController<RefreshRequest, RefreshResponse>
{
    [HttpPost]
    public override async Task<RefreshResponse> Invoke(
        [FromBody] RefreshRequest request
    )
    {
        var facadeArgs =
            new RefreshArgs(
                request.RefreshToken
            );

        var result =
            await
                facade
                    .Execute(
                        facadeArgs
                    );

        var response =
            new RefreshResponse(
                result.AccessToken,
                result.RefreshToken,
                result.AccessTokenExpiresAt
            );

        return
            response;
    }
}