using System;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.Locations;

namespace FashionFace.Repositories.Context.Models.Filters;

public sealed class FilterLocation : EntityBase
{
    public required Guid CityId { get; set; }
    public required Guid FilterId { get; set; }
    public required Guid PlaceId { get; set; }

    public required LocationType LocationType { get; set; }

    public City? City { get; set; }
    public Filter? Filter { get; set; }
    public Place? Place { get; set; }
}