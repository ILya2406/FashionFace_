using System.Linq;
using System.Threading.Tasks;

using FashionFace.Controllers.Anonymous.Implementations.Base;
using FashionFace.Controllers.Anonymous.Responses.Models.PoseReferences;
using FashionFace.Controllers.Base.Responses.Models;
using FashionFace.Facades.Anonymous.Interfaces.PoseReferences;

using Microsoft.AspNetCore.Mvc;

namespace FashionFace.Controllers.Anonymous.Implementations.PoseReferences;

[Route(
    "api/v1/pose-references"
)]
public sealed class PoseReferenceListController(
    IPoseReferenceListFacade facade
) : AnonymousControllerBase
{
    [HttpGet]
    public async Task<ListResponse<PoseReferenceListItemResponse>> Invoke()
    {
        var args = new FashionFace.Facades.Anonymous.Args.PoseReferences.PoseReferenceListArgs();

        var result =
            await
                facade
                    .Execute(
                        args
                    );

        var itemList =
            result
                .ItemList
                .Select(
                    entity =>
                        new PoseReferenceListItemResponse(
                            entity.Id,
                            entity.Description,
                            FormatMediaUrl(entity.PreviewImageUrl),
                            entity.ModelUrl != null ? FormatMediaUrl(entity.ModelUrl) : null
                        )
                )
                .ToList();

        var response =
            new ListResponse<PoseReferenceListItemResponse>(
                result.TotalCount,
                itemList
            );

        return
            response;
    }

    private static string FormatMediaUrl(string url)
    {
        if (url.StartsWith("http://") || url.StartsWith("https://"))
            return url;
        return "/files" + url;
    }
}
