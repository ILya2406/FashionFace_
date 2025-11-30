using System.Threading.Tasks;

using FashionFace.Controllers.Base.Attributes.Groups;
using FashionFace.Controllers.Base.Implementations.Base;
using FashionFace.Controllers.Users.Requests.Models;
using FashionFace.Controllers.Users.Responses.Models;
using FashionFace.Facades.Users.Args;
using FashionFace.Facades.Users.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Users.Implementations;

[UserControllerGroup("Generate")]
[Route(
    "api/v1/user/generate"
)]
public sealed class UserGenerateController(
    IGenerateFacade facade
) : BaseAuthorizeController
{
    [HttpPost]
    public async Task<GenerateResponse> Invoke(
        [FromBody] GenerateRequest request
    )
    {
        var facadeArgs =
            new GenerateArgs(
                request.Prompt
            );

        var result =
            await
                facade
                    .Execute(
                        facadeArgs
                    );

        var response =
            new GenerateResponse(
                result.TaskId
            );

        return
            response;
    }
}