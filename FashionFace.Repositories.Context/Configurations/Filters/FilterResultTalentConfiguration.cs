using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Filters;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Filters;

public sealed class FilterResultTalentConfiguration : EntityBaseConfiguration<FilterResultTalent>
{
    public override void Configure(EntityTypeBuilder<FilterResultTalent> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.FilterResultId
            )
            .HasColumnName(
                "FilterResultId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.TalentId
            )
            .HasColumnName(
                "TalentId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.IsValidated
            )
            .HasColumnName(
                "IsValidated"
            )
            .HasColumnType(
                "boolean"
            )
            .HasDefaultValue(
                true
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.FilterResult
            )
            .WithMany(
                entity => entity.FilterResultTalentCollection
            )
            .HasForeignKey(
                entity => entity.FilterResultId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.Talent
            )
            .WithMany(
                entity => entity.FilterResultTalentCollection
            )
            .HasForeignKey(
                entity => entity.TalentId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}