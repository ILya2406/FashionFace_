using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Repositories.Context.Interfaces;

public interface IWithTaskStatus
{
    TaskStatus TaskStatus { get; set; }
}
