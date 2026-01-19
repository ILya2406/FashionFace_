using System;
using System.Collections.Generic;
using System.Linq;

using FashionFace.Repositories.Context;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Models;

using Microsoft.EntityFrameworkCore;

using Npgsql;

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
                parameterList
                    .Select(
                        parameter =>
                            new NpgsqlParameter(
                                parameter.ParameterName,
                                parameter.Value ?? DBNull.Value
                            )
                    )
                    .ToArray()
            );
}