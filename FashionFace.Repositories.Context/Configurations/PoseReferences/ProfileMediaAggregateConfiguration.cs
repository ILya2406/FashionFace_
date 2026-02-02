using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.PoseReferences;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.PoseReferences;

public sealed class PoseReferenceMediaAggregateConfiguration : EntityConfigurationBase<PoseReferenceMediaAggregate>
{
    public override void Configure(EntityTypeBuilder<PoseReferenceMediaAggregate> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.PoseReferenceId
            )
            .HasColumnName(
                nameof(PoseReferenceMediaAggregate.PoseReferenceId)
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
                nameof(PoseReferenceMediaAggregate.MediaAggregateId)
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.PoseReference
            )
            .WithOne(
                entity => entity.PoseReferenceMediaAggregate
            )
            .HasForeignKey<PoseReferenceMediaAggregate>(
                entity => entity.PoseReferenceId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.MediaAggregate
            )
            .WithOne()
            .HasForeignKey<PoseReferenceMediaAggregate>(
                entity => entity.MediaAggregateId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}