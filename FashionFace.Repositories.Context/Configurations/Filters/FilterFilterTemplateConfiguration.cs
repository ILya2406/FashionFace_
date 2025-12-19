using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Filters;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Filters;

public sealed class FilterFilterTemplateConfiguration : EntityBaseConfiguration<FilterFilterTemplate>
{
    public override void Configure(EntityTypeBuilder<FilterFilterTemplate> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.FilterId
            )
            .HasColumnName(
                "FilterId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.FilterTemplateId
            )
            .HasColumnName(
                "FilterTemplateId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.Filter
            )
            .WithOne(
            )
            .HasForeignKey<FilterFilterTemplate>(
                entity => entity.FilterId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.FilterTemplate
            )
            .WithOne(
            )
            .HasForeignKey<FilterFilterTemplate>(
                entity => entity.FilterTemplateId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}