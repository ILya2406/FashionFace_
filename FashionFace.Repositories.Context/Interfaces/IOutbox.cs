namespace FashionFace.Repositories.Context.Interfaces;

public interface IOutbox :
    IWithOutboxStatus,
    IWithAttemptCount,
    IWithClaimedAt,
    IWithCorrelationId,
    IWithCreatedAt;