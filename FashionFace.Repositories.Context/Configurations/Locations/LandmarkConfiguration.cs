using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Locations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Locations;

public sealed class LandmarkConfiguration : EntityBaseConfiguration<Landmark>
{
    public override void Configure(EntityTypeBuilder<Landmark> builder)
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
                "varchar(128)"
            )
            .IsRequired();
    }
}