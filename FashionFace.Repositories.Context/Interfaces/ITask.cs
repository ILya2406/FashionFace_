namespace FashionFace.Repositories.Context.Interfaces;

public interface ITask :
    IWithTaskStatus,
    IWithAttemptCount,
    IWithClaimedAt,
    IWithCorrelationId,
    IWithCreatedAt;
