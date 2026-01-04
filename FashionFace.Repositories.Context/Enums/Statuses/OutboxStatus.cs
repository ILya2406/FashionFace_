namespace FashionFace.Repositories.Context.Enums;

public enum OutboxStatus
{
    Pending,
    Claimed,
    Done,
    Failed,
}