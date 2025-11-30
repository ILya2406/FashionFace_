using FashionFace.Controllers.Base.Attributes.Groups.Base;

namespace FashionFace.Controllers.Base.Attributes.Groups;

public sealed class UserControllerGroupAttribute : ControllerGroupAttribute
{
    public UserControllerGroupAttribute(
        string groupSubName
    ) : base(
        $"User/{groupSubName}"
    ) { }
}