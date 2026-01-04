using System.Linq.Expressions;

using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Base;

public abstract class EntityConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : EntityBase
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        var type =
            typeof(TEntity);

        builder.ToTable(
            type.Name
        );

        var isWithIdentifier =
            type
                .IsAssignableFrom(
                    typeof(IWithIdentifier)
                );

        if (isWithIdentifier)
        {
            var parameter = Expression.Parameter(
                type,
                "entity"
            );
            var property = Expression.Property(
                parameter,
                nameof(IWithIdentifier.Id)
            );
            var lambda = Expression.Lambda(
                property,
                parameter
            );

            builder.HasKey(
                (dynamic)lambda
            );

            builder
                .Property(
                    (dynamic)lambda
                )
                .HasColumnName(
                    nameof(IWithIdentifier.Id)
                )
                .HasColumnType(
                    "uuid"
                )
                .ValueGeneratedOnAdd();
        }

        var isWithIDeleted =
            type
                .IsAssignableFrom(
                    typeof(IWithIsDeleted)
                );

        if (isWithIDeleted)
        {
            var parameter = Expression.Parameter(
                type,
                "entity"
            );
            var property = Expression.Property(
                parameter,
                nameof(IWithIsDeleted.IsDeleted)
            );
            var lambda = Expression.Lambda(
                property,
                parameter
            );

            builder
                .Property(
                    (dynamic)lambda
                )
                .HasColumnName(
                    nameof(IWithIsDeleted.IsDeleted)
                )
                .HasColumnType(
                    "boolean"
                )
                .IsRequired();
        }

        var isWithPositionIndex =
            type
                .IsAssignableFrom(
                    typeof(IWithPositionIndex)
                );

        if (isWithPositionIndex)
        {
            var parameter = Expression.Parameter(
                type,
                "entity"
            );
            var property = Expression.Property(
                parameter,
                nameof(IWithPositionIndex.PositionIndex)
            );
            var lambda = Expression.Lambda(
                property,
                parameter
            );

            builder
                .Property(
                    (dynamic)lambda
                )
                .HasColumnName(
                    nameof(IWithPositionIndex.PositionIndex)
                )
                .HasColumnType(
                    "double precision"
                )
                .IsRequired();
        }

        var isWithCreatedAt =
            type
                .IsAssignableFrom(
                    typeof(IWithCreatedAt)
                );

        if (isWithCreatedAt)
        {
            var parameter = Expression.Parameter(
                type,
                "entity"
            );
            var property = Expression.Property(
                parameter,
                nameof(IWithCreatedAt.CreatedAt)
            );
            var lambda = Expression.Lambda(
                property,
                parameter
            );

            builder
                .Property(
                    (dynamic)lambda
                )
                .HasColumnName(
                    nameof(IWithCreatedAt.CreatedAt)
                )
                .HasColumnType(
                    "timestamp with time zone"
                )
                .IsRequired();
        }

        var isWithOutboxStatus =
            type
                .IsAssignableFrom(
                    typeof(IWithOutboxStatus)
                );

        if (isWithOutboxStatus)
        {
            var parameter = Expression.Parameter(
                type,
                "entity"
            );
            var property = Expression.Property(
                parameter,
                nameof(IWithOutboxStatus.OutboxStatus)
            );
            var lambda = Expression.Lambda(
                property,
                parameter
            );

            builder
                .Property(
                    (dynamic)lambda
                )
                .HasColumnName(
                    nameof(IWithOutboxStatus.OutboxStatus)
                )
                .HasConversion<string>()
                .HasColumnType(
                    "varchar(16)"
                )
                .IsRequired();
        }

        var isWithAttemptCount =
            type
                .IsAssignableFrom(
                    typeof(IWithAttemptCount)
                );

        if (isWithAttemptCount)
        {
            var parameter = Expression.Parameter(
                type,
                "entity"
            );
            var property = Expression.Property(
                parameter,
                nameof(IWithAttemptCount.AttemptCount)
            );
            var lambda = Expression.Lambda(
                property,
                parameter
            );

            builder
                .Property(
                    (dynamic)lambda
                )
                .HasColumnName(
                    nameof(IWithAttemptCount.AttemptCount)
                )
                .HasColumnType(
                    "integer"
                )
                .IsRequired();
        }

        var isWithProcessingStartedAt =
            type
                .IsAssignableFrom(
                    typeof(IWithProcessingStartedAt)
                );

        if (isWithProcessingStartedAt)
        {
            var parameter = Expression.Parameter(
                type,
                "entity"
            );
            var property = Expression.Property(
                parameter,
                nameof(IWithProcessingStartedAt.ProcessingStartedAt)
            );
            var lambda = Expression.Lambda(
                property,
                parameter
            );

            builder
                .Property(
                    (dynamic)lambda
                )
                .HasColumnName(
                    nameof(IWithProcessingStartedAt.ProcessingStartedAt)
                )
                .HasColumnType(
                    "timestamp with time zone"
                )
                .IsRequired();
        }
    }
}