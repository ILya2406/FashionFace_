using System;

namespace FashionFace.Controllers.Users.Requests.Models.DossierEntities;

public sealed record UserDossierMediaCreateRequest(
    Guid MediaId
);