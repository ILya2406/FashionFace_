using System;

namespace FashionFace.Repositories.Context.Models;

public sealed class Landmark : EntityBase
{
    public required Guid PlaceId { get; set; }

    public required string Name { get; set; }

    public Place? Place { get; set; }
}