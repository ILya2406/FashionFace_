using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models.Locations;

public sealed class Building : EntityBase
{
    public required string Name { get; set; }

    public Place? Place { get; set; }
}