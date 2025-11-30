using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Base.Attributes.Groups.Base;

public abstract class ControllerGroupAttribute : ApiExplorerSettingsAttribute
{
    protected ControllerGroupAttribute(
        string fullGroupName
    )
    {
        GroupName =
            fullGroupName;

        IgnoreApi =
            false;
    }
}