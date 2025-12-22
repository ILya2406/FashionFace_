using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Filters;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Filters;

public sealed class DimensionValueConfiguration : EntityBaseConfiguration<DimensionValue>
{
    public override void Configure(EntityTypeBuilder<DimensionValue> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.DimensionId
            )
            .HasColumnName(
                "DimensionId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.Code
            )
            .HasColumnName(
                "Code"
            )
            .HasColumnType(
                "varchar(128)"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.Dimension
            )
            .WithMany(
                entity => entity.DimensionValueCollection
            )
            .HasForeignKey(
                entity => entity.DimensionId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasIndex(
                entity =>
                    new
                    {
                        entity.DimensionId,
                        entity.Code,
                    }
            )
            .IsUnique();
    }
}