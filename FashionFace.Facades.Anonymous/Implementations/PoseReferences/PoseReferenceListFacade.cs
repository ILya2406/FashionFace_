using System.Linq;
using System.Threading.Tasks;

using FashionFace.Facades.Anonymous.Args.PoseReferences;
using FashionFace.Facades.Anonymous.Interfaces.PoseReferences;
using FashionFace.Facades.Anonymous.Models.PoseReferences;
using FashionFace.Facades.Base.Models;
using FashionFace.Repositories.Context.Models.PoseReferences;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Anonymous.Implementations.PoseReferences;

public sealed class PoseReferenceListFacade(
    IGenericReadRepository genericReadRepository
) : IPoseReferenceListFacade
{
    public async Task<ListResult<PoseReferenceListItemResult>> Execute(
        PoseReferenceListArgs args
    )
    {
        var poseReferenceCollection =
            genericReadRepository.GetCollection<PoseReference>();

        var poseReferenceList =
            await
                poseReferenceCollection
                    .Include(pr => pr.FileResource)
                    .Include(pr => pr.ModelFileResource)
                    .OrderBy(pr => pr.CreatedAt)
                    .Select(
                        entity =>
                            new PoseReferenceListItemResult(
                                entity.Id,
                                entity.Description,
                                entity.FileResource != null ? entity.FileResource.RelativePath : string.Empty,
                                entity.ModelFileResource != null ? entity.ModelFileResource.RelativePath : null
                            )
                    )
                    .ToListAsync();

        var totalCount = poseReferenceList.Count;

        var result =
            new ListResult<PoseReferenceListItemResult>(
                totalCount,
                poseReferenceList
            );

        return
            result;
    }
}
