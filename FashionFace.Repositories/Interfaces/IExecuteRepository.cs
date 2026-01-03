using System.Collections.Generic;
using System.Linq;

using FashionFace.Repositories.Interfaces.Base;
using FashionFace.Repositories.Models;

namespace FashionFace.Repositories.Interfaces;

public interface IExecuteRepository : IRepository
{
    IQueryable<TEntity> FromSqlRaw<TEntity>(
        string sql,
        IReadOnlyList<SqlParameter> parameterList
    )
        where TEntity : class;
}