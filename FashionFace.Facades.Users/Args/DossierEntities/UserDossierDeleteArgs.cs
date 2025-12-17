using System;

namespace FashionFace.Facades.Users.Args.DossierEntities;

public sealed record UserDossierDeleteArgs(
    Guid UserId
);