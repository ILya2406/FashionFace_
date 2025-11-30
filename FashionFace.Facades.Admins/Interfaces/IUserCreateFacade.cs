using FashionFace.Facades.Admins.Args;
using FashionFace.Facades.Admins.Models;
using FashionFace.Facades.Base.Interfaces;

namespace FashionFace.Facades.Admins.Interfaces;

public interface IUserCreateFacade :
    IQueryFacade
    <
        UserCreateArgs,
        UserCreateResult
    >;