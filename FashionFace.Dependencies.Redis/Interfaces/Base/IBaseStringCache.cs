namespace FashionFace.Dependencies.Redis.Interfaces.Base;

public interface IBaseStringCache<TEntity> : IBaseCache<string, TEntity>
    where TEntity : class { }