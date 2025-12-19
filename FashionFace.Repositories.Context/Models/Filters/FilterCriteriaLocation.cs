using System;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.Locations;

namespace FashionFace.Repositories.Context.Models.Filters;

public sealed class FilterCriteriaLocation : EntityBase
{
    public required Guid FilterCriteriaId { get; set; }
    public required Guid CityId { get; set; }
    public required Guid PlaceId { get; set; }

    public required LocationType LocationType { get; set; }

    public City? City { get; set; }
    public Place? Place { get; set; }
    public FilterCriteria? FilterCriteria { get; set; }
}