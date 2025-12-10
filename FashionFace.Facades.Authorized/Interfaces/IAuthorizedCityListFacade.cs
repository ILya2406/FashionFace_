using FashionFace.Facades.Authorized.Args;
using FashionFace.Facades.Authorized.Models;
using FashionFace.Facades.Base.Interfaces;
using FashionFace.Facades.Base.Models;

namespace FashionFace.Facades.Authorized.Interfaces;

public interface IAuthorizedCityListFacade :
    IQueryFacade
    <
        AuthorizedCityListArgs,
        ListResult<AuthorizedCityListItemResult>
    >;