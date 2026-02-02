using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.RenderPipelines;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.RenderPipelines;

public sealed class RenderPipelineConfiguration : EntityConfigurationBase<RenderPipeline>
{
    public override void Configure(EntityTypeBuilder<RenderPipeline> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.TalentId
            )
            .HasColumnName(
                nameof(RenderPipeline.TalentId)
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.PoseReferenceId
            )
            .HasColumnName(
                nameof(RenderPipeline.PoseReferenceId)
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.ProductMediaAggregateId
            )
            .HasColumnName(
                nameof(RenderPipeline.ProductMediaAggregateId)
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired(false);

        builder
            .HasOne(
                entity => entity.ApplicationUser
            )
            .WithMany()
            .HasForeignKey<RenderPipeline>(
                entity => entity.ApplicationUserId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.Talent
            )
            .WithMany()
            .HasForeignKey<RenderPipeline>(
                entity => entity.TalentId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.PoseReference
            )
            .WithMany()
            .HasForeignKey<RenderPipeline>(
                entity => entity.PoseReferenceId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.ProductMediaAggregate
            )
            .WithMany()
            .HasForeignKey<RenderPipeline>(
                entity => entity.ProductMediaAggregateId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            )
            .IsRequired(false);
    }
}