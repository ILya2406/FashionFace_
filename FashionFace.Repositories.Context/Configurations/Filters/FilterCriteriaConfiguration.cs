using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Filters;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Filters;

public sealed class FilterCriteriaConfiguration : EntityConfigurationBase<FilterCriteria>
{
    public override void Configure(EntityTypeBuilder<FilterCriteria> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.TalentType
            )
            .HasColumnName(
                "TalentType"
            )
            .HasConversion<string>()
            .HasColumnType(
                "varchar(32)"
            )
            .IsRequired(false);
    }
}