using Microsoft.AspNetCore.Authorization;

namespace FashionFace.Controllers.Base.Implementations.Base;

[AllowAnonymous]
public abstract class BaseAnonymousController :
    BaseController;