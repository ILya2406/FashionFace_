using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.MediaEntities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.MediaEntities;

public sealed class MediaFileConfiguration : EntityBaseConfiguration<MediaFile>
{
    public override void Configure(EntityTypeBuilder<MediaFile> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.ProfileId
            )
            .HasColumnName(
                "ProfileId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.RelativePath
            )
            .HasColumnName(
                "RelativePath"
            )
            .HasColumnType(
                "text"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.Profile
            )
            .WithMany(
                entity => entity.MediaFileCollection
            )
            .HasForeignKey(
                entity => entity.ProfileId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}