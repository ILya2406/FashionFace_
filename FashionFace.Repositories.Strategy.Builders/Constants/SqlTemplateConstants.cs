namespace FashionFace.Repositories.Strategy.Builders.Constants;

public static class SqlTemplateConstants
{
    public const string SelectPendingForClaim =
        """
            SELECT *
            FROM "{0}"
            WHERE "Status" = @Status and "ProcessingStartedAt" is null
            ORDER BY "MessageCreatedAt"
            FOR UPDATE SKIP LOCKED
            LIMIT @BatchCount
        """;

    public const string SelectClaimedForRetry =
        """
            SELECT *
            FROM "{0}"
            WHERE "Status" = @Status and "ProcessingStartedAt" < @ProcessingStartedAt
            ORDER BY "MessageCreatedAt"
            FOR UPDATE SKIP LOCKED
            LIMIT @BatchCount
        """;

}