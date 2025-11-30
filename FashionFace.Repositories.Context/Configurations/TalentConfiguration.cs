using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations;

public sealed class TalentConfiguration : EntityBaseConfiguration<Talent>
{
    public override void Configure(EntityTypeBuilder<Talent> builder)
    {
        base.Configure(builder);

        builder
            .Property(entity => entity.Type)
            .HasColumnName("Type")
            .HasConversion<string>()
            .HasColumnType("varchar(32)")
            .IsRequired();

        builder
            .Property(entity => entity.ProfileId)
            .HasColumnName("ProfileId")
            .HasColumnType("uuid")
            .IsRequired();

        builder
            .HasOne(entity => entity.Profile)
            .WithMany(entity => entity.TalentCollection)
            .HasForeignKey(entity => entity.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}