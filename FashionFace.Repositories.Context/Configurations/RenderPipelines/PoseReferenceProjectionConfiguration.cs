using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.RenderPipelines;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.RenderPipelines;

public sealed class PoseReferenceProjectionConfiguration : EntityConfigurationBase<PoseReferenceProjection>
{
    public override void Configure(EntityTypeBuilder<PoseReferenceProjection> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.PoseReferenceId
            )
            .HasColumnName(
                nameof(PoseReferenceProjection.PoseReferenceId)
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
                nameof(PoseReferenceProjection.MediaAggregateId)
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.PoseReference
            )
            .WithMany(
                pr => pr.PoseReferenceProjectionCollection
            )
            .HasForeignKey(
                entity => entity.PoseReferenceId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.MediaAggregate
            )
            .WithMany()
            .HasForeignKey(
                entity => entity.MediaAggregateId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}