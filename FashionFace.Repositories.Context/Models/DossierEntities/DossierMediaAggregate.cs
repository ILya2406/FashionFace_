using System;

using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.MediaEntities;

namespace FashionFace.Repositories.Context.Models.DossierEntities;

public sealed class DossierMediaAggregate : EntityBase,
    IWithPositionIndex
{
    public required Guid DossierId { get; set; }
    public required Guid MediaAggregateId { get; set; }

    public Dossier? Dossier { get; set; }
    public MediaAggregate? MediaAggregate { get; set; }

    public required double PositionIndex { get; set; }
}