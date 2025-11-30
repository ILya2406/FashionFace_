using Microsoft.AspNetCore.Authorization;

namespace FashionFace.Controllers.Users.Attributes;

public sealed class AuthorizeAdminAttribute : AuthorizeAttribute
{
    public AuthorizeAdminAttribute() =>
        Roles = "Admin";
}