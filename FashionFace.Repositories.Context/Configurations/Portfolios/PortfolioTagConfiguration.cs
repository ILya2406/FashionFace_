using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Portfolios;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Portfolios;

public sealed class PortfolioTagConfiguration : EntityBaseConfiguration<PortfolioTag>
{
    public override void Configure(EntityTypeBuilder<PortfolioTag> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.PortfolioId
            )
            .HasColumnName(
                "PortfolioMediaAggregateId"
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
                "double precision"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.Portfolio
            )
            .WithMany(
                entity => entity.PortfolioTagCollection
            )
            .HasForeignKey(
                entity => entity.PortfolioId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.Tag
            )
            .WithMany(
                entity => entity.PortfolioTagCollection
            )
            .HasForeignKey(
                entity => entity.TagId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}