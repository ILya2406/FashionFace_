using FashionFace.Facades.Base.Interfaces;
using FashionFace.Facades.Domains.Args;
using FashionFace.Facades.Domains.Models;

namespace FashionFace.Facades.Domains.Interfaces;

public interface IAuthenticationModelCreateFacade :
    IQueryFacade
    <
        AuthenticationModelCreateArgs,
        AuthenticationModel
    >;