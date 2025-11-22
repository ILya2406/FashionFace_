using System.Threading.Tasks;

using FashionFace.Repositories.Context;
using FashionFace.Repositories.Transactions.Interfaces;

using Microsoft.EntityFrameworkCore.Storage;

namespace FashionFace.Repositories.Transactions.Implementations;

public sealed class Transaction(
    ApplicationDatabaseContext context,
    IDbContextTransaction transaction
) : ITransaction
{
    public async Task CommitAsync()
    {
        await
            context.SaveChangesAsync();

        await
            transaction.CommitAsync();
    }

    public async Task Rollback() =>
        await transaction.RollbackAsync();

    public void Dispose() =>
        transaction.Dispose();
}