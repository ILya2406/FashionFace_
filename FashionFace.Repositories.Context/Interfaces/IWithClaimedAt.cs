using System;

namespace FashionFace.Repositories.Context.Interfaces;

public interface IWithClaimedAt
{
    DateTime? ClaimedAt { get; set; }
}