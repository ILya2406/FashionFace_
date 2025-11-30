using FashionFace.Controllers.Base.Attributes.Groups.Base;

namespace FashionFace.Controllers.Base.Attributes.Groups;

public sealed class AuthorizedControllerGroupAttribute : ControllerGroupAttribute
{
    public AuthorizedControllerGroupAttribute(
        string groupSubName
    ) : base(
        $"Authorized/{groupSubName}"
    ) { }
}