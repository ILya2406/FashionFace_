using System;

namespace FashionFace.Repositories.Context.Models;

public sealed class Pseudonym : EntityBase
{
    public required Guid TalentId { get; set; }

    public required string Name { get; set; }
    public required string Description { get; set; }

    public Talent? Talent { get; set; }
}