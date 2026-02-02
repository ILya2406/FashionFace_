using System;

namespace FashionFace.Repositories.Context.Interfaces;

public interface IWithApplicationUserId
{
    Guid ApplicationUserId { get; set; }
}