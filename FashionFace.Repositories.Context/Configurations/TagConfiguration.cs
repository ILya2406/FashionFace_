using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations;

public sealed class TagConfiguration : EntityBaseConfiguration<Tag>
{
    public override void Configure(EntityTypeBuilder<Tag> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.Name
            )
            .HasColumnName(
                "Name"
            )
            .HasColumnType(
                "varchar(32)"
            )
            .IsRequired();
    }
}