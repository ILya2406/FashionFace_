using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations;

public sealed class PlaceConfiguration : EntityBaseConfiguration<Place>
{
    public override void Configure(EntityTypeBuilder<Place> builder)
    {
        base.Configure(builder);

        builder
            .Property(entity => entity.Street)
            .HasColumnName("Street")
            .HasColumnType("varchar(128)")
            .IsRequired();
    }
}