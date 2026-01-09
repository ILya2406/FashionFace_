using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Talents;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Talents;

public sealed class TalentConfiguration : EntityConfigurationBase<Talent>
{
    public override void Configure(EntityTypeBuilder<Talent> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.Description
            )
            .HasColumnName(
                "LocationType"
            )
            .HasColumnType(
                "varchar(32)"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.TalentType
            )
            .HasColumnName(
                "Description"
            )
            .HasConversion<string>()
            .HasColumnType(
                "varchar(1024)"
            )
            .IsRequired();
    }
}