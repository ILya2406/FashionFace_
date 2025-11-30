namespace FashionFace.Repositories.Context.Models;

public sealed class Place : EntityBase
{
    public required string Street { get; set; }

    public Building? Building { get; set; }
    public Landmark? Landmark { get; set; }
    public TalentLocation? TalentLocation { get; set; }
}