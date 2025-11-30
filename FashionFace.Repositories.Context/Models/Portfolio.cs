using System;

namespace FashionFace.Repositories.Context.Models;

public sealed class Portfolio : EntityBase
{
    public required Guid TalentId { get; set; }

    public required string Url { get; set; }
    public required string Description { get; set; }

    public Talent? Talent { get; set; }
}