using FashionFace.Repositories.Context.Configurations.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.AppearanceTraits;

public sealed class AppearanceTraitsConfiguration : EntityBaseConfiguration<Models.AppearanceTraits.AppearanceTraits>
{
    public override void Configure(EntityTypeBuilder<Models.AppearanceTraits.AppearanceTraits> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.ProfileId
            )
            .HasColumnName(
                "ProfileId"
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
            )
            .IsRequired();

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
            )
            .IsRequired();

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
            )
            .IsRequired();

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
            )
            .IsRequired();

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
            )
            .IsRequired();

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
            )
            .IsRequired();

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
            )
            .IsRequired();

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
            )
            .IsRequired();

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
            )
            .IsRequired();

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
            )
            .IsRequired();

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
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.Height
            )
            .HasColumnName(
                "Height"
            )
            .HasColumnType(
                "integer"
            )
            .IsRequired();

        builder
            .Property(
                entity => entity.ShoeSize
            )
            .HasColumnName(
                "ShoeSize"
            )
            .HasColumnType(
                "integer"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.Profile
            )
            .WithOne(
                entity => entity.AppearanceTraits
            )
            .HasForeignKey<Models.AppearanceTraits.AppearanceTraits>(
                entity => entity.ProfileId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}