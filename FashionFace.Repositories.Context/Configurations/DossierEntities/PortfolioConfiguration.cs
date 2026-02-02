using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.DossierEntities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.DossierEntities;

public sealed class DossierConfiguration : EntityConfigurationBase<Dossier>
{
    public override void Configure(EntityTypeBuilder<Dossier> builder)
    {
        base.Configure(builder);

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
            .HasOne(
                entity => entity.Profile
            )
            .WithOne(
                entity => entity.Dossier
            )
            .HasForeignKey<Dossier>(
                entity => entity.ProfileId
            )
            .OnDelete(
                DeleteBehavior.Cascade
            );
    }
}