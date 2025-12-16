using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.Filters;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Filters;

public sealed class FilterAppearanceTraitsConfiguration : EntityBaseConfiguration<FilterAppearanceTraits>
{
    public override void Configure(EntityTypeBuilder<FilterAppearanceTraits> builder)
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
                entity => entity.Filter
            )
            .WithOne(
                entity => entity.FilterAppearanceTraits
            )
            .HasForeignKey<FilterAppearanceTraits>(
                entity => entity.FilterId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}