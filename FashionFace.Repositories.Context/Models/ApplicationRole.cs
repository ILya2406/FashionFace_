using System;

using Microsoft.AspNetCore.Identity;

namespace FashionFace.Repositories.Context.Models;

public sealed class ApplicationRole : IdentityRole<Guid> { }

/*public sealed class ApplicationRoleConfiguration :
    IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(
        EntityTypeBuilder<ApplicationRole> builder
    )
    {
        builder
            .ToTable(
                "ApplicationRole"
            );

        builder
            .HasKey(
                entity => entity.Id
            );

        builder
            .Property(
                entity => entity.Id
            )
            .HasColumnType(
                "uuid"
            )
            .HasColumnName(
                "Id"
            );
    }
}*/