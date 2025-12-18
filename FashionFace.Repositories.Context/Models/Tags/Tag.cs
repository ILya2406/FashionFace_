using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models.Tags;

public sealed class Tag : EntityBase
{
    public required string Name { get; set; }
}