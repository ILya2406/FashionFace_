using System;

namespace FashionFace.Facades.Users.Args.DossierEntities;

public sealed record UserDossierMediaDeleteArgs(
    Guid UserId,
    Guid MediaId
);