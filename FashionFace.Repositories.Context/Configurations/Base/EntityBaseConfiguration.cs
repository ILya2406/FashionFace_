using FashionFace.Repositories.Context.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFace.Repositories.Context.Configurations.Base;

public abstract class EntityBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : EntityBase
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(typeof(TEntity).Name);

        builder.HasKey(entity => entity.Id);

        builder
            .Property(entity => entity.Id)
            .HasColumnName("Id")
            .HasColumnType("uuid")
            .ValueGeneratedOnAdd();
    }
}