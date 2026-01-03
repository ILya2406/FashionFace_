using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Repositories.Strategy.Args;

public sealed record PostgresOutboxBatchStrategyArgs(
    OutboxStatus Status,
    int BatchSize
);