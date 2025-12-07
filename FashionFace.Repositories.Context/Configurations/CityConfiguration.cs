using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations;

public sealed class CityConfiguration : EntityBaseConfiguration<City>
{
    public override void Configure(EntityTypeBuilder<City> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.Country
            )
            .HasColumnName(
                "Country"
            )
            .HasColumnType(
                "varchar(128)"
            )
            .IsRequired();

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

        builder
            .HasIndex(
                entity => new
                {
                    entity.Country,
                    entity.Name,
                }
            )
            .IsUnique();
    }
}