using System;
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;

namespace FashionFace.Controllers.Implementations.Base;

[Authorize]
public abstract class BaseAuthorizeController<TRequest, TResponse> :
    BaseController<TRequest, TResponse>
{
    protected Guid GetUserId()
    {
        var httpContextUser =
            HttpContext.User;

        var userIdString =
            httpContextUser
                .FindFirstValue(
                    ClaimTypes.NameIdentifier
                );

        var userId =
            Guid
                .Parse(
                    userIdString
                    ?? string.Empty
                );

        return
            userId;
    }
}