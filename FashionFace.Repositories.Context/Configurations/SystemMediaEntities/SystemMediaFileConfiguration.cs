using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.SystemMediaEntities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.SystemMediaEntities;

public sealed class SystemMediaFileConfiguration : EntityConfigurationBase<SystemMediaFile>
{
    public override void Configure(EntityTypeBuilder<SystemMediaFile> builder)
    {
        base.Configure(
            builder
        );

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
