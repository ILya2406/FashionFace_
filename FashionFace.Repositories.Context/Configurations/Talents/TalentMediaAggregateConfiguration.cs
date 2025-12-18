using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Talents;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Talents;

public sealed class TalentMediaAggregateConfiguration : EntityBaseConfiguration<TalentMediaAggregate>
{
    public override void Configure(EntityTypeBuilder<TalentMediaAggregate> builder)
    {
        base.Configure(
            builder
        );

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
                entity => entity.MediaAggregateId
            )
            .HasColumnName(
                "MediaAggregateId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.Talent
            )
            .WithOne(
                entity => entity.TalentMediaAggregate
            )
            .HasForeignKey<TalentMediaAggregate>(
                entity => entity.TalentId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.MediaAggregate
            )
            .WithOne()
            .HasForeignKey<TalentMediaAggregate>(
                entity => entity.MediaAggregateId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}