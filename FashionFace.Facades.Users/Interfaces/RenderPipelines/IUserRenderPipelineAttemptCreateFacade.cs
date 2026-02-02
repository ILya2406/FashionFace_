using FashionFace.Facades.Base.Interfaces;
using FashionFace.Facades.Users.Args.RenderPipelines;
using FashionFace.Facades.Users.Models.RenderPipelines;

namespace FashionFace.Facades.Users.Interfaces.RenderPipelines;

public interface IUserRenderPipelineAttemptCreateFacade :
    IQueryFacade
    <
        UserRenderPipelineAttemptCreateArgs,
        UserRenderPipelineAttemptCreateResult
    >;