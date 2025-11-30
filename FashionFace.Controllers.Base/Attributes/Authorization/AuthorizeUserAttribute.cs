using Microsoft.AspNetCore.Authorization;

namespace FashionFace.Controllers.Base.Attributes.Authorization;

public sealed class AuthorizeUserAttribute : AuthorizeAttribute
{
    public AuthorizeUserAttribute() =>
        Roles = "User";
}