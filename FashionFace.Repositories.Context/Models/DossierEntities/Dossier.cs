using System;
using System.Collections.Generic;

using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.Profiles;

namespace FashionFace.Repositories.Context.Models.DossierEntities;

public sealed class Dossier : EntityBase,
    IWithIsDeleted
{
    public required Guid ProfileId { get; set; }

    public ICollection<DossierMediaAggregate> DossierMediaCollection { get; set; }

    public Profile? Profile { get; set; }

    public required bool IsDeleted { get; set; }
}