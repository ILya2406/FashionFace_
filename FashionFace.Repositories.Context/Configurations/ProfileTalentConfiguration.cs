using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models;
using FashionFace.Repositories.Context.Models.Profiles;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations;

public sealed class ProfileTalentConfiguration : EntityBaseConfiguration<ProfileTalent>
{
    public override void Configure(EntityTypeBuilder<ProfileTalent> builder)
    {
        base.Configure(
            builder
        );

        builder
            .Property(
                entity => entity.ProfileId
            )
            .HasColumnName(
                "ProfileMediaId"
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
                entity => entity.PositionIndex
            )
            .HasColumnName(
                "PositionIndex"
            )
            .HasColumnType(
                "double precision"
            )
            .IsRequired();

        builder
            .HasOne(
                entity => entity.Profile
            )
            .WithMany(
                entity => entity.ProfileTalentCollection
            )
            .HasForeignKey(
                entity => entity.ProfileId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );

        builder
            .HasOne(
                entity => entity.Talent
            )
            .WithOne(
                entity => entity.ProfileTalent
            )
            .HasForeignKey<ProfileTalent>(
                entity => entity.TalentId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}