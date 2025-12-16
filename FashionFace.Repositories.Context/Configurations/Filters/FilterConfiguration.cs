using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Filters;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Filters;

public sealed class FilterConfiguration : EntityBaseConfiguration<Filter>
{
    public override void Configure(EntityTypeBuilder<Filter> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.ApplicationUserId
            )
            .HasColumnName(
                "ApplicationUserId"
            )
            .HasColumnType(
                "uuid"
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
                "varchar(64)"
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

        builder
            .HasOne(
                entity => entity.ApplicationUser
            )
            .WithMany()
            .HasForeignKey(
                entity => entity.ApplicationUserId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}