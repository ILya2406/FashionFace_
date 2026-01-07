using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.Portfolios;
using FashionFace.Facades.Users.Args.Portfolios;
using FashionFace.Facades.Users.Interfaces.Portfolios;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations.Portfolios;

[UserControllerGroup(
    "Portfolio"
)]
[Route(
    "api/v1/user/portfolio/media/position"
)]
public sealed class UserPortfolioMediaPositionUpdateController(
    IUserPortfolioMediaPositionUpdateFacade facade
) : UserControllerBase
{
    [HttpPatch]
    public async Task Invoke(
        [FromBody] UserPortfolioMediaPositionUpdateRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserPortfolioMediaPositionUpdateArgs(
                userId,
                request.MediaId,
                request.PositionIndex
            );

        await
            facade
                .Execute(
                    facadeArgs
                );
    }
}
