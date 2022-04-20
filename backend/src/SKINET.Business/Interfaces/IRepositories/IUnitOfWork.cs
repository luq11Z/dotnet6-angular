using SKINET.Business.Models;

namespace SKINET.Business.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : Entity;
        Task<int> Complete(); //returns the number of changes in the data base
    }
}
