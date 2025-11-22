using System.Threading.Tasks;

namespace FashionFace.Repositories.Transactions.Interfaces;

public interface ITransactionManager
{
    Task<ITransaction> BeginTransaction();
}