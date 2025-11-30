
using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations;

public sealed class PortfolioConfiguration : EntityBaseConfiguration<Portfolio>
{
    public override void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        base.Configure(builder);

        builder
            .Property(entity => entity.Url)
            .HasColumnName("Url")
            .HasColumnType("varchar(1024)")
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
            .WithOne(entity => entity.Portfolio)
            .HasForeignKey<Portfolio>(entity => entity.TalentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}