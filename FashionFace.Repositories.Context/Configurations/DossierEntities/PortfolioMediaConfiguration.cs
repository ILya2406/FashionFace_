using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.DossierEntities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.DossierEntities;

public sealed class DossierMediaConfiguration : EntityBaseConfiguration<DossierMediaAggregate>
{
    public override void Configure(EntityTypeBuilder<DossierMediaAggregate> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.DossierId
            )
            .HasColumnName(
                "DossierId"
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
                entity => entity.Dossier
            )
            .WithMany(
                entity => entity.DossierMediaCollection
            )
            .HasForeignKey(
                entity => entity.DossierId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.MediaAggregate
            )
            .WithOne()
            .HasForeignKey<DossierMediaAggregate>(
                entity => entity.MediaAggregateId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}