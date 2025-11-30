using FashionFace.Facades.Anonymous.Args;
using FashionFace.Facades.Anonymous.Models;
using FashionFace.Facades.Base.Interfaces;

namespace FashionFace.Facades.Anonymous.Interfaces;

public interface ILoginFacade :
    IQueryFacade
    <
        LoginArgs,
        LoginResult
    >;