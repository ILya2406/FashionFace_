using FashionFace.Facades.Base.Interfaces;
using FashionFace.Facades.Users.Args.Locations;
using FashionFace.Facades.Users.Models.Locations;

namespace FashionFace.Facades.Users.Interfaces.Locations;

public interface IUserLocationCreateFacade :
    IQueryFacade
    <
        UserLocationCreateArgs,
        UserLocationCreateResult
    >;