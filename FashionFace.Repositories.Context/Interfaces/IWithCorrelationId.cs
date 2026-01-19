using System;

namespace FashionFace.Repositories.Context.Interfaces;

public interface IWithCorrelationId
{
    Guid CorrelationId { get; set; }
}