using FashionFace.Controllers.Base.Attributes.Groups.Base;

namespace FashionFace.Controllers.Base.Attributes.Groups;

public sealed class ProfileControllerGroupAttribute : ControllerGroupAttribute
{
    public ProfileControllerGroupAttribute()
        : base("./Profile")
    {
    }
}
