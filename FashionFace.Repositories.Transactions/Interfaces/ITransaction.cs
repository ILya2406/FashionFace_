using System;
using System.Threading.Tasks;

namespace FashionFace.Repositories.Transactions.Interfaces;

public interface ITransaction : IDisposable
{
    Task CommitAsync();

    Task Rollback();
}