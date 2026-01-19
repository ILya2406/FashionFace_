using System;
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
            typeof(IWithIdentifier)
                .IsAssignableFrom(
                    type
                );

        if (isWithIdentifier)
        {
            // Проверяем, что у типа действительно есть свойство Id
            var idProperty = type.GetProperty(nameof(IWithIdentifier.Id));

            if (idProperty != null)
            {
                var parameter = Expression.Parameter(
                    type,
                    "entity"
                );
                var property = Expression.Property(
                    parameter,
                    nameof(IWithIdentifier.Id)
                );

                // Создаем лямбду для HasKey: Expression<Func<TEntity, object>>
                var funcTypeObject = typeof(Func<,>).MakeGenericType(type, typeof(object));
                var lambdaForKey = Expression.Lambda(
                    funcTypeObject,
                    Expression.Convert(property, typeof(object)),
                    parameter
                );

                builder.HasKey(
                    (dynamic)lambdaForKey
                );

                // Используем строковое имя свойства для Property
                builder
                    .Property(nameof(IWithIdentifier.Id))
                    .HasColumnName(
                        nameof(IWithIdentifier.Id)
                    )
                    .HasColumnType(
                        "uuid"
                    )
                    .ValueGeneratedOnAdd();
            }
        }

        var isWithIDeleted =
            typeof(IWithIsDeleted)
                .IsAssignableFrom(
                    type
                );

        if (isWithIDeleted)
        {
            builder
                .Property(nameof(IWithIsDeleted.IsDeleted))
                .HasColumnName(
                    nameof(IWithIsDeleted.IsDeleted)
                )
                .HasColumnType(
                    "boolean"
                )
                .IsRequired();
        }

        var isWithPositionIndex =
            typeof(IWithPositionIndex)
                .IsAssignableFrom(
                    type
                );

        if (isWithPositionIndex)
        {
            builder
                .Property(nameof(IWithPositionIndex.PositionIndex))
                .HasColumnName(
                    nameof(IWithPositionIndex.PositionIndex)
                )
                .HasColumnType(
                    "double precision"
                )
                .IsRequired();
        }

        var isWithCreatedAt =
            typeof(IWithCreatedAt)
                .IsAssignableFrom(
                    type
                );

        if (isWithCreatedAt)
        {
            builder
                .Property(nameof(IWithCreatedAt.CreatedAt))
                .HasColumnName(
                    nameof(IWithCreatedAt.CreatedAt)
                )
                .HasColumnType(
                    "timestamp with time zone"
                )
                .IsRequired();
        }

        var isWithOutboxStatus =
            typeof(IWithOutboxStatus)
                .IsAssignableFrom(
                    type
                );

        if (isWithOutboxStatus)
        {
            builder
                .Property(nameof(IWithOutboxStatus.OutboxStatus))
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
            typeof(IWithAttemptCount)
                .IsAssignableFrom(
                    type
                );

        if (isWithAttemptCount)
        {
            builder
                .Property(nameof(IWithAttemptCount.AttemptCount))
                .HasColumnName(
                    nameof(IWithAttemptCount.AttemptCount)
                )
                .HasColumnType(
                    "integer"
                )
                .IsRequired();
        }

        var isWithProcessingStartedAt =
            typeof(IWithProcessingStartedAt)
                .IsAssignableFrom(
                    type
                );

        if (isWithProcessingStartedAt)
        {
            builder
                .Property(nameof(IWithProcessingStartedAt.ProcessingStartedAt))
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