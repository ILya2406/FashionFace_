using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models.Portfolios;
using FashionFace.Controllers.Users.Responses.Models.Portfolios;
using FashionFace.Facades.Users.Args.Portfolios;
using FashionFace.Facades.Users.Interfaces.Portfolios;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations.Portfolios;

[UserControllerGroup(
    "Portfolio"
)]
[Route(
    "api/v1/user/portfolio"
)]
public sealed class UserPortfolioController(
    IUserPortfolioFacade facade
) : UserControllerBase
{
    [HttpGet]
    public async Task<UserPortfolioResponse> Invoke(
        [FromQuery] UserPortfolioRequest request
    )
    {
        var userId =
            GetUserId();

        var facadeArgs =
            new UserPortfolioArgs(
                userId,
                request.TalentId
            );

        var result =
            await
                facade
                    .Execute(
                        facadeArgs
                    );

        var response =
            new UserPortfolioResponse(
                result.Id,
                result.TalentId,
                result.Description
            );

        return
            response;
    }
}