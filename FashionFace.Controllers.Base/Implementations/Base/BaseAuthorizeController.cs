using System;
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;

namespace FashionFace.Controllers.Base.Implementations.Base;

[Authorize]
public abstract class BaseAuthorizeController :
    BaseController
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