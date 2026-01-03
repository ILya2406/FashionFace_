using System.Collections.Generic;
using System.Linq;

using FashionFace.Repositories.Context;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Models;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Repositories.Implementations;

public sealed class ExecuteRepository(
    ApplicationDatabaseContext context
) : IExecuteRepository
{
    public IQueryable<TEntity> FromSqlRaw<TEntity>(
        string sql,
        IReadOnlyList<SqlParameter> parameterList
    )
        where TEntity : class =>
        context
            .Set<TEntity>()
            .FromSqlRaw(
                sql,
                parameterList.ToArray()
            );
}