using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Users.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models;
using FashionFace.Controllers.Users.Responses.Models;
using FashionFace.Facades.Users.Args;
using FashionFace.Facades.Users.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations;

[UserControllerGroup(
    "Portfolio"
)]
[Route(
    "api/v1/user/portfolio"
)]
public sealed class UserPortfolioController(
    IUserPortfolioFacade facade
) : BaseUserController
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
                result.Description
            );

        return
            response;
    }
}