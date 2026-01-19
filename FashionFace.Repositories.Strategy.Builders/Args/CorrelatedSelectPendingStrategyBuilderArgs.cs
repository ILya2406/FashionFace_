using System;

namespace FashionFace.Repositories.Strategy.Builders.Args;

public sealed record CorrelatedSelectPendingStrategyBuilderArgs(
    Guid CorrelationId,
    int BatchSize
);