using FashionFace.Facades.Base.Interfaces;
using FashionFace.Facades.Base.Models;
using FashionFace.Facades.Users.Args.Filters;
using FashionFace.Facades.Users.Models.Filters;

namespace FashionFace.Facades.Users.Interfaces.Filters;

public interface IUserFilterListFacade :
    IQueryFacade
    <
        UserFilterListArgs,
        ListResult<UserFilterListItemResult>
    >;