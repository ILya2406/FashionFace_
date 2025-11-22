using System;
using System.Reflection;

using FashionFace.Repositories.Context.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FashionFace.Repositories.Context;

public sealed class ApplicationDatabaseContext(
    DbContextOptions<ApplicationDatabaseContext> options
) : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(
    options
)
{
    protected override void OnModelCreating(
        ModelBuilder modelBuilder
    )
    {
        base.OnModelCreating(
            modelBuilder
        );

        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly()
        );
    }
}