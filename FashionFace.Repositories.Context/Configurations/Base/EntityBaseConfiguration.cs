using System.Linq.Expressions;

using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Base;

public abstract class EntityBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
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
            var parameter = Expression.Parameter(type, "entity");
            var property = Expression.Property(parameter, nameof(IWithIdentifier.Id));
            var lambda = Expression.Lambda(property, parameter);

            builder.HasKey(
                (dynamic)lambda
            );

            builder
                .Property(
                    (dynamic)lambda
                )
                .HasColumnName(
                    "Id"
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
            var parameter = Expression.Parameter(type, "entity");
            var property = Expression.Property(parameter, nameof(IWithIsDeleted.IsDeleted));
            var lambda = Expression.Lambda(property, parameter);

            builder
                .Property(
                    (dynamic)lambda
                )
                .HasColumnName(
                    "IsDeleted"
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
            var parameter = Expression.Parameter(type, "entity");
            var property = Expression.Property(parameter, nameof(IWithPositionIndex.PositionIndex));
            var lambda = Expression.Lambda(property, parameter);

            builder
                .Property(
                    (dynamic)lambda
                )
                .HasColumnName(
                    "PositionIndex"
                )
                .HasColumnType(
                    "double precision"
                )
                .IsRequired();

        }
    }
}