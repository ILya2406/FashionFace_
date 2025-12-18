using FashionFace.Facades.Base.Interfaces;
using FashionFace.Facades.Base.Models;
using FashionFace.Facades.Users.Args.Filters;
using FashionFace.Facades.Users.Models.Portfolios;

namespace FashionFace.Facades.Users.Interfaces.Filters;

public interface IUserFilterResultListFacade :
    IQueryFacade
    <
        UserFilterResultListArgs,
        ListResult<UserMediaListItemResult>
    >;