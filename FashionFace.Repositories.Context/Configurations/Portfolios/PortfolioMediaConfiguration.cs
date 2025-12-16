using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Portfolios;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Portfolios;

public sealed class PortfolioMediaConfiguration : EntityBaseConfiguration<PortfolioMediaAggregate>
{
    public override void Configure(EntityTypeBuilder<PortfolioMediaAggregate> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.PortfolioId
            )
            .HasColumnName(
                "PortfolioId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.MediaAggregateId
            )
            .HasColumnName(
                "MediaAggregateId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.Portfolio
            )
            .WithMany(
                entity => entity.PortfolioMediaCollection
            )
            .HasForeignKey(
                entity => entity.PortfolioId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.MediaAggregate
            )
            .WithOne()
            .HasForeignKey<PortfolioMediaAggregate>(
                entity => entity.MediaAggregateId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}