using Microsoft.AspNetCore.Authorization;

namespace FashionFace.Controllers.Implementations.Base;

[AllowAnonymous]
public abstract class BaseAnonymousController<TRequest, TResponse> :
    BaseController<TRequest, TResponse>;