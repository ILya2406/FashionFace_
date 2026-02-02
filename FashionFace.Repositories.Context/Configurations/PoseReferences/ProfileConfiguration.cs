using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.PoseReferences;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.PoseReferences;

public sealed class PoseReferenceConfiguration : EntityConfigurationBase<PoseReference>
{
    public override void Configure(EntityTypeBuilder<PoseReference> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.FileResourceId
            )
            .HasColumnName(
                nameof(PoseReference.FileResourceId)
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.ModelFileResourceId
            )
            .HasColumnName(
                nameof(PoseReference.ModelFileResourceId)
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired(false);

        builder
            .HasOne(
                entity => entity.FileResource
            )
            .WithOne()
            .HasForeignKey<PoseReference>(
                entity => entity.FileResourceId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.ModelFileResource
            )
            .WithMany()
            .HasForeignKey(
                entity => entity.ModelFileResourceId
            )
            .OnDelete(
                DeleteBehavior.SetNull
            );
    }
}