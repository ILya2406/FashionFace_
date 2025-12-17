using FashionFace.Facades.Base.Interfaces;
using FashionFace.Facades.Users.Args.AppearanceTraitsEntities;
using FashionFace.Facades.Users.Models.AppearanceTraitsEntities;

namespace FashionFace.Facades.Users.Interfaces.AppearanceTraitsEntities;

public interface IUserFemaleTraitsFacade :
    IQueryFacade
    <
        UserFemaleTraitsArgs,
        UserFemaleTraitsResult
    >;