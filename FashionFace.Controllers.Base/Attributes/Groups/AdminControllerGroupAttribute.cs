using FashionFace.Controllers.Base.Attributes.Groups.Base;

namespace FashionFace.Controllers.Base.Attributes.Groups;

public sealed class AdminControllerGroupAttribute : ControllerGroupAttribute
{
    public AdminControllerGroupAttribute(
        string groupSubName
    ) : base(
        $"Admin/{groupSubName}"
    ) { }
}