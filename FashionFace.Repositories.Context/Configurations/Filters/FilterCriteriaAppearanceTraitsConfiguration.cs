using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Filters;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Filters;

public sealed class FilterCriteriaAppearanceTraitsConfiguration : EntityBaseConfiguration<FilterCriteriaAppearanceTraits>
{
    public override void Configure(EntityTypeBuilder<FilterCriteriaAppearanceTraits> builder)
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
            .Property(
                entity => entity.SexType
            )
            .HasColumnName(
                "SexType"
            )
            .HasConversion<string>()
            .HasColumnType(
                "varchar(32)"
            );

        builder
            .Property(
                entity => entity.FaceType
            )
            .HasColumnName(
                "FaceType"
            )
            .HasConversion<string>()
            .HasColumnType(
                "varchar(32)"
            );

        builder
            .Property(
                entity => entity.HairColorType
            )
            .HasColumnName(
                "HairColorType"
            )
            .HasConversion<string>()
            .HasColumnType(
                "varchar(32)"
            );

        builder
            .Property(
                entity => entity.HairType
            )
            .HasColumnName(
                "HairType"
            )
            .HasConversion<string>()
            .HasColumnType(
                "varchar(32)"
            );

        builder
            .Property(
                entity => entity.HairLengthType
            )
            .HasColumnName(
                "HairLengthType"
            )
            .HasConversion<string>()
            .HasColumnType(
                "varchar(32)"
            );

        builder
            .Property(
                entity => entity.BodyType
            )
            .HasColumnName(
                "BodyType"
            )
            .HasConversion<string>()
            .HasColumnType(
                "varchar(32)"
            );

        builder
            .Property(
                entity => entity.SkinToneType
            )
            .HasColumnName(
                "SkinToneType"
            )
            .HasConversion<string>()
            .HasColumnType(
                "varchar(32)"
            );

        builder
            .Property(
                entity => entity.EyeShapeType
            )
            .HasColumnName(
                "EyeShapeType"
            )
            .HasConversion<string>()
            .HasColumnType(
                "varchar(32)"
            );

        builder
            .Property(
                entity => entity.EyeColorType
            )
            .HasColumnName(
                "EyeColorType"
            )
            .HasConversion<string>()
            .HasColumnType(
                "varchar(32)"
            );

        builder
            .Property(
                entity => entity.NoseType
            )
            .HasColumnName(
                "NoseType"
            )
            .HasConversion<string>()
            .HasColumnType(
                "varchar(32)"
            );

        builder
            .Property(
                entity => entity.JawType
            )
            .HasColumnName(
                "JawType"
            )
            .HasConversion<string>()
            .HasColumnType(
                "varchar(32)"
            );

        builder
            .Property(
                entity => entity.Height
            )
            .HasColumnName(
                "Height"
            )
            .HasColumnType(
                "integer"
            );

        builder
            .Property(
                entity => entity.ShoeSize
            )
            .HasColumnName(
                "ShoeSize"
            )
            .HasColumnType(
                "integer"
            );

        builder
            .HasOne(
                entity => entity.FilterCriteria
            )
            .WithOne(
                entity => entity.FilterCriteriaAppearanceTraits
            )
            .HasForeignKey<FilterCriteriaAppearanceTraits>(
                entity => entity.FilterCriteriaId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}