using System.Threading.Tasks;

using FashionFace.Repositories.Context;
using FashionFace.Repositories.Transactions.Interfaces;

namespace FashionFace.Repositories.Transactions.Implementations;

public sealed class TransactionManager(
    ApplicationDatabaseContext context
) : ITransactionManager
{
    public async Task<ITransaction> BeginTransaction()
    {
        var transactionDb =
            await
                context
                    .Database
                    .BeginTransactionAsync();

        return
            new Transaction(
                context,
                transactionDb
            );
    }
}