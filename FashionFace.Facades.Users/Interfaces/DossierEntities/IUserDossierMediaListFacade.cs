using FashionFace.Facades.Base.Interfaces;
using FashionFace.Facades.Base.Models;
using FashionFace.Facades.Users.Args.DossierEntities;
using FashionFace.Facades.Users.Models.Portfolios;

namespace FashionFace.Facades.Users.Interfaces.DossierEntities;

public interface IUserDossierMediaListFacade :
    IQueryFacade
    <
        UserDossierMediaListArgs,
        ListResult<UserMediaListItemResult>
    >;