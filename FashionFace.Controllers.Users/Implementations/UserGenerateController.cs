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
    "Generate"
)]
[Route(
    "api/v1/user/generate"
)]
public sealed class UserGenerateController(
    IUserGenerateFacade facade
) : BaseUserController
{
    [ApiExplorerSettings(
        IgnoreApi = true
    )]
    [HttpPost]
    public async Task<UserGenerateResponse> Invoke(
        [FromBody] UserGenerateRequest request
    )
    {
        var facadeArgs =
            new UserGenerateArgs(
                request.Prompt
            );

        var result =
            await
                facade
                    .Execute(
                        facadeArgs
                    );

        var response =
            new UserGenerateResponse(
                result.TaskId
            );

        return
            response;
    }
}