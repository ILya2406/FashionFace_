using System;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models;

public sealed class TalentLocation : EntityBase
{
    public required Guid TalentId { get; set; }
    public required Guid CityId { get; set; }

    public required LocationType Type { get; set; }

    public Guid? PlaceId { get; set; }

    public City? City { get; set; }
    public Place? Place { get; set; }
    public Talent? Talent { get; set; }
}