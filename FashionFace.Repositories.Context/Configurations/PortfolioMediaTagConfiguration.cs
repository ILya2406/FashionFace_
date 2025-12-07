using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations;

public sealed class PortfolioMediaTagConfiguration : EntityBaseConfiguration<PortfolioMediaTag>
{
    public override void Configure(EntityTypeBuilder<PortfolioMediaTag> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.PortfolioMediaId
            )
            .HasColumnName(
                "PortfolioMediaId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.TagId
            )
            .HasColumnName(
                "TagId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.PositionIndex
            )
            .HasColumnName(
                "PositionIndex"
            )
            .HasColumnType(
                "integer"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.PortfolioMedia
            )
            .WithMany(
                entity => entity.PortfolioMediaTagCollection
            )
            .HasForeignKey(
                entity => entity.PortfolioMediaId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.Tag
            )
            .WithMany(
                entity => entity.PortfolioMediaTagCollection
            )
            .HasForeignKey(
                entity => entity.TagId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}