using Microsoft.AspNetCore.Authorization;

namespace FashionFace.Controllers.Base.Attributes.Authorization;

public sealed class AuthorizeAdminAttribute : AuthorizeAttribute
{
    public AuthorizeAdminAttribute() =>
        Roles = "Admin";
}