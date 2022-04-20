using SKINET.Business.Interfaces;
using SKINET.Business.Interfaces.Repositories;
using SKINET.Business.Models;
using SKINET.Data.Context;
using System.Collections;

namespace SKINET.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;
        private Hashtable _repositories;

        public UnitOfWork(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : Entity
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _storeContext);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<TEntity>)_repositories[type];
        }

        public async Task<int> Complete()
        {
            return await _storeContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _storeContext.Dispose();
        }
    }
}
