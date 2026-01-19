using FashionFace.Repositories.Context.Configurations.Base;
using FashionFace.Repositories.Context.Models.DossierEntities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.DossierEntities;

public sealed class DossierConfiguration : EntityConfigurationBase<Dossier>
{
    public override void Configure(EntityTypeBuilder<Dossier> builder)
    {
        // НЕ вызываем base.Configure(), так как Dossier использует ProfileId как ключ, а не Id

        // Устанавливаем первичный ключ - ProfileId
        builder
            .HasKey(
                entity => entity.ProfileId
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

        // Настраиваем IsDeleted (из IWithIsDeleted)
        builder
            .Property(
                entity => entity.IsDeleted
            )
            .HasColumnName(
                "IsDeleted"
            )
            .HasColumnType(
                "boolean"
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