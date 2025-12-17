using System;

namespace FashionFace.Controllers.Users.Requests.Models.DossierEntities;

public sealed record UserDossierMediaDeleteRequest(
    Guid MediaId
);