using Microsoft.AspNetCore.Authorization;

namespace FashionFace.Controllers.Users.Attributes;

public sealed class AuthorizeUserAttribute : AuthorizeAttribute
{
    public AuthorizeUserAttribute() =>
        Roles = "User";
}