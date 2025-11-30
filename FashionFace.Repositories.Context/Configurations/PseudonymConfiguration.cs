
using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations;

public sealed class PseudonymConfiguration : EntityBaseConfiguration<Pseudonym>
{
    public override void Configure(EntityTypeBuilder<Pseudonym> builder)
    {
        base.Configure(builder);

        builder
            .Property(entity => entity.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar(128)")
            .IsRequired();

        builder
            .Property(entity => entity.Description)
            .HasColumnName("Description")
            .HasColumnType("text")
            .IsRequired();

        builder
            .Property(entity => entity.TalentId)
            .HasColumnName("TalentId")
            .HasColumnType("uuid")
            .IsRequired();

        builder
            .HasOne(entity => entity.Talent)
            .WithOne(entity => entity.Pseudonym)
            .HasForeignKey<Pseudonym>(entity => entity.TalentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}