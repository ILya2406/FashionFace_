using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Filters;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Filters;

public sealed class FilterResultConfiguration : EntityBaseConfiguration<FilterResult>
{
    public override void Configure(EntityTypeBuilder<FilterResult> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.FilterId
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
                entity => entity.FilterResultStatus
            )
            .HasColumnName(
                "FilterResultStatus"
            )
            .HasConversion<string>()
            .HasColumnType(
                "varchar(64)"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.Filter
            )
            .WithOne(
                entity => entity.FilterResult
            )
            .HasForeignKey<FilterResult>(
                entity => entity.FilterId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}