using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.MediaEntities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.MediaEntities;

public sealed class MediaFileConfiguration : EntityConfigurationBase<MediaFile>
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
                entity => entity.FileResourceId
            )
            .HasColumnName(
                "FileResourceId"
            )
            .HasColumnType(
                "uuid"
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

        builder
            .HasOne(
                entity => entity.FileResource
            )
            .WithMany()
            .HasForeignKey(
                entity => entity.FileResourceId
            )
            .OnDelete(
                DeleteBehavior.SetNull
            );
    }
}