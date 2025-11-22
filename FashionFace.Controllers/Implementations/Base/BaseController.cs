using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Implementations.Base;

[ApiVersion(
    "1.0"
)]
[ApiController]
public abstract class BaseController<TRequest, TResponse> : ControllerBase
{
    public abstract Task<TResponse> Invoke(
        TRequest request
    );
}