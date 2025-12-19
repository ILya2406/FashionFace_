using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Filters;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Filters;

public sealed class FilterTemplateConfiguration : EntityBaseConfiguration<FilterTemplate>
{
    public override void Configure(EntityTypeBuilder<FilterTemplate> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.FilterCriteriaId
            )
            .HasColumnName(
                "FilterCriteriaId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.FilterCriteria
            )
            .WithOne()
            .HasForeignKey<FilterTemplate>(
                entity => entity.FilterCriteriaId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}