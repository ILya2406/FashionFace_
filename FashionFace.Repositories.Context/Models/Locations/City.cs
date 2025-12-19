using System.Collections.Generic;

using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.Filters;

namespace FashionFace.Repositories.Context.Models.Locations;

public sealed class City : EntityBase
{
    public required string Country { get; set; }
    public required string Name { get; set; }

    public ICollection<Location> LocationCollection { get; set; }
    public ICollection<FilterCriteriaLocation> FilterLocationCollection { get; set; }
}