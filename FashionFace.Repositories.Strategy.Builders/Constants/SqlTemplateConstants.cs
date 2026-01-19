namespace FashionFace.Repositories.Strategy.Builders.Constants;

public static class SqlTemplateConstants
{
    public const string CorrelatedSelectPendingForClaim =
        """
            SELECT *
            FROM "{0}"
            WHERE "OutboxStatus" = @OutboxStatus and "CorrelationId" = @CorrelationId and "ClaimedAt" is null
            ORDER BY "CreatedAt" DESC
            FOR UPDATE SKIP LOCKED
            LIMIT @BatchSize
        """;

    public const string SelectPendingForClaim =
        """
            SELECT *
            FROM "{0}"
            WHERE "OutboxStatus" = @OutboxStatus and "ClaimedAt" is null
            ORDER BY "CreatedAt" DESC
            FOR UPDATE SKIP LOCKED
            LIMIT @BatchSize
        """;

    public const string SelectClaimedForRetry =
        """
            SELECT *
            FROM "{0}"
            WHERE "OutboxStatus" = @OutboxStatus and "ClaimedAt" < @ClaimedAt
            ORDER BY "CreatedAt" DESC
            FOR UPDATE SKIP LOCKED
            LIMIT @BatchSize
        """;

}
