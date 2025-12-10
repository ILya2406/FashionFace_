using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models;
using FashionFace.Facades.Users.Args;
using FashionFace.Facades.Users.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations;

[UserControllerGroup(
    "Portfolio"
)]
[Route(
    "api/v1/user/portfolio/Media"
)]
public sealed class UserPortfolioMediaCreateController(
    IUserPortfolioMediaCreateFacade facade
) : BaseUserController
{
    [ApiExplorerSettings(
        IgnoreApi = true
    )]
    [HttpPost]
    public async Task Invoke(
        [FromBody] UserPortfolioMediaCreateRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserPortfolioMediaCreateArgs(
                userId,
                request.MediaId,
                request.PortfolioId
            );

        await
            facade
                .Execute(
                    facadeArgs
                );
    }
}