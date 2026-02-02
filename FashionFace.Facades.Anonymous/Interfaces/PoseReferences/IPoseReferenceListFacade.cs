using System.Threading.Tasks;

using FashionFace.Facades.Anonymous.Args.PoseReferences;
using FashionFace.Facades.Anonymous.Models.PoseReferences;
using FashionFace.Facades.Base.Interfaces;
using FashionFace.Facades.Base.Models;

namespace FashionFace.Facades.Anonymous.Interfaces.PoseReferences;

public interface IPoseReferenceListFacade :
    IQueryFacade
    <
        PoseReferenceListArgs,
        ListResult<PoseReferenceListItemResult>
    >;
