using System.Threading.Tasks;

using FashionFace.Controllers.Anonymous.Requests.Models;
using FashionFace.Controllers.Anonymous.Responses.Models;
using FashionFace.Controllers.Base.Implementations.Base;
using FashionFace.Facades.Anonymous.Args;
using FashionFace.Facades.Anonymous.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Anonymous.Implementations.Authentication;

[Route(
    "api/v1/refresh"
)]
public sealed class RefreshController(
    IRefreshFacade facade
) : BaseAnonymousController
{
    [HttpPost]
    public async Task<RefreshResponse> Invoke(
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