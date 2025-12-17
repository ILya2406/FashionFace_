using FashionFace.Facades.Base.Interfaces;
using FashionFace.Facades.Users.Args.DossierEntities;
using FashionFace.Facades.Users.Models.DossierEntities;

namespace FashionFace.Facades.Users.Interfaces.DossierEntities;

public interface IUserDossierFacade :
    IQueryFacade
    <
        UserDossierArgs,
        UserDossierResult
    >;