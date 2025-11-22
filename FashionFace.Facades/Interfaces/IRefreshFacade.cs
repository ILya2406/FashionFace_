using FashionFace.Facades.Args;
using FashionFace.Facades.Base.Interfaces;
using FashionFace.Facades.Models;

namespace FashionFace.Facades.Interfaces;

public interface IRefreshFacade :
    IQueryFacade
    <
        RefreshArgs,
        RefreshResult
    >;