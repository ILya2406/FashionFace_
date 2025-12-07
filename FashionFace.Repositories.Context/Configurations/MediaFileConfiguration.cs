using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations;

public sealed class MediaFileConfiguration : EntityBaseConfiguration<MediaFile>
{
    public override void Configure(EntityTypeBuilder<MediaFile> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.Uri
            )
            .HasColumnName(
                "Uri"
            )
            .HasColumnType(
                "text"
            )
            .IsRequired();
    }
}