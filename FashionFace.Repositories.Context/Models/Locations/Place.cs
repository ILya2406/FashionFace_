using System;

using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.Filters;

namespace FashionFace.Repositories.Context.Models.Locations;

public sealed class Place : EntityBase
{
    public required Guid BuildingId { get; set; }
    public required Guid LandmarkId { get; set; }

    public required string Street { get; set; }

    public Building? Building { get; set; }
    public Landmark? Landmark { get; set; }

    public Location? Location { get; set; }
    public FilterCriteriaLocation? FilterLocation { get; set; }
}