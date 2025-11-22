using System.Threading.Tasks;

using FashionFace.Controllers.Implementations.Base;
using FashionFace.Controllers.Models;
using FashionFace.Facades.Args;
using FashionFace.Facades.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Implementations;

[Route(
    "api/v1/generate"
)]
public sealed class GenerateController(
    IGenerateFacade facade
) : BaseAuthorizeController<GenerateRequest, GenerateResponse>
{
    [HttpPost]
    public override async Task<GenerateResponse> Invoke(
        [FromBody] GenerateRequest request
    )
    {
        var generateArgs =
            new GenerateArgs(
                request.Prompt
            );

        var result =
            await
                facade
                    .Execute(
                        generateArgs
                    );

        var response =
            new GenerateResponse(
                result.TaskId
            );

        return
            response;
    }
}