using System.Collections.Generic;

using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models;

public sealed class City : EntityBase
{
    public required string Country { get; set; }
    public required string Name { get; set; }

    public ICollection<TalentLocation> TalentLocationCollection { get; set; }
}