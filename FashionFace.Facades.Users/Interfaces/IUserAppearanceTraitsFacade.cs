using FashionFace.Facades.Base.Interfaces;
using FashionFace.Facades.Users.Args;
using FashionFace.Facades.Users.Models;

namespace FashionFace.Facades.Users.Interfaces;

public interface IUserAppearanceTraitsFacade :
    IQueryFacade
    <
        UserAppearanceTraitsArgs,
        UserAppearanceTraitsResult
    >;