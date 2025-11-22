using FashionFace.Repositories.Transactions.Implementations;
using FashionFace.Repositories.Transactions.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace FashionFace.Repositories.Transactions;

public static class TransactionCollectionExtensions
{
    public static void AddTransactions(
        this IServiceCollection services
    )
    {
        services.AddScoped<ITransactionManager, TransactionManager>();
    }
}