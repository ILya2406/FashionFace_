using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Repositories.Context.Interfaces;

public interface IWithOutboxStatus
{
    OutboxStatus OutboxStatus { get; set; }
}
