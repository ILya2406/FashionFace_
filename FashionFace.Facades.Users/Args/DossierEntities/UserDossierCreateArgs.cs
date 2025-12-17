using System;

namespace FashionFace.Facades.Users.Args.DossierEntities;

public sealed record UserDossierCreateArgs(
    Guid UserId
);