using System;

using FashionFace.Repositories.Context.Interfaces;

namespace FashionFace.Repositories.Context.Models.Base;

public class EntityBase : IWithIdentifier
{
    public required Guid Id { get; set; }
}