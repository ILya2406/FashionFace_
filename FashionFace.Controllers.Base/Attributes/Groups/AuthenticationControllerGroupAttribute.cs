using FashionFace.Controllers.Base.Attributes.Groups.Base;

namespace FashionFace.Controllers.Base.Attributes.Groups;

public sealed class AuthenticationControllerGroupAttribute : ControllerGroupAttribute
{
    public AuthenticationControllerGroupAttribute() : base(
        "./Authentication"
    ) { }
}