using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.AppearanceTraitsEntities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.AppearanceTraitsEntities;

public sealed class FemaleTraitsConfiguration : EntityConfigurationBase<FemaleTraits>
{
    public override void Configure(EntityTypeBuilder<FemaleTraits> builder)
    {
        // НЕ вызываем base.Configure(), так как FemaleTraits использует AppearanceTraitsId как ключ, а не Id

        // Устанавливаем первичный ключ - AppearanceTraitsId
        builder
            .HasKey(
                entity => entity.AppearanceTraitsId
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
                entity => entity.BustSizeType
            )
            .HasColumnName(
                "BustSizeType"
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
                entity => entity.FemaleTraits
            )
            .HasForeignKey<FemaleTraits>(
                entity => entity.AppearanceTraitsId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}