using FashionFace.Dependencies.Redis.Args;
using FashionFace.Dependencies.Redis.Interfaces.Base;
using FashionFace.Dependencies.Redis.Models;

namespace FashionFace.Dependencies.Redis.Interfaces;

public interface IFilterCursorCache :
    IBaseCache
    <
        FilterCursorCacheArgs,
        FilterCursorCacheModel
    >;