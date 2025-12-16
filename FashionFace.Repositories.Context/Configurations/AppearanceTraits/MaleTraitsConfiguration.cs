using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.AppearanceTraits;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.AppearanceTraits;

public sealed class MaleTraitsConfiguration : EntityBaseConfiguration<MaleTraits>
{
    public override void Configure(EntityTypeBuilder<MaleTraits> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.AppearanceTraitsId
            )
            .HasColumnName(
                "AppearanceTraitsId"
            )
            .HasColumnType(
                "uuid"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.FacialHairLengthType
            )
            .HasColumnName(
                "FacialHairLengthType"
            )
            .HasConversion<string>()
            .HasColumnType(
                "varchar(32)"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.AppearanceTraits
            )
            .WithOne(
                entity => entity.MaleTraits
            )
            .HasForeignKey<MaleTraits>(
                entity => entity.AppearanceTraitsId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}