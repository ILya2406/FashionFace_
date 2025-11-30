using System;

namespace FashionFace.Repositories.Context.Interfaces;

public interface IWithIdentifier
{
    Guid Id { get; set; }
}