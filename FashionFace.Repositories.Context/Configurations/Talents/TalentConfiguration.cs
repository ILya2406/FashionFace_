using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Talents;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Talents;

public sealed class TalentConfiguration : EntityBaseConfiguration<Talent>
{
    public override void Configure(EntityTypeBuilder<Talent> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.IsDeleted
            )
            .HasColumnName(
                "IsDeleted"
            )
            .HasColumnType(
                "boolean"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.Description
            )
            .HasColumnName(
                "Description"
            )
            .HasColumnType(
                "varchar(1024)"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.TalentType
            )
            .HasColumnName(
                "LocationType"
            )
            .HasConversion<string>()
            .HasColumnType(
                "varchar(32)"
            )
            .IsRequired();
    }
}