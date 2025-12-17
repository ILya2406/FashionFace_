using System;

namespace FashionFace.Facades.Users.Args.DossierEntities;

public sealed record UserDossierMediaCreateArgs(
    Guid UserId,
    Guid MediaId
);