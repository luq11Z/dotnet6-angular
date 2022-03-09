using Microsoft.EntityFrameworkCore;
using SKINET.Business.Interfaces;
using SKINET.Business.Models;
using SKINET.Data.Context;
using SKINET.Data.Specifications;
using System.Linq.Expressions;

namespace SKINET.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        /* Only inherited classes or repositories will be able to use. */
        protected readonly StoreContext dbContext;

        /* 'Shortcut' to dbset. */
        protected readonly DbSet<TEntity> dbSet;

        public Repository(StoreContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        /* virtual allows overrinding */
        public virtual async Task<TEntity> GetById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetWithSpec(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        public virtual async Task<List<TEntity>> GetAllWithSpec(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task Add(TEntity entity)
        {
            dbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Update(TEntity entity)
        {
            dbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Delete(int id)
        {
            dbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dbContext?.Dispose();
        }

        public async Task<int> Count(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).CountAsync();
        }

        /* Apply the criterias desired */
        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            return EvaluatorSpecification<TEntity>.GetQuery(dbContext.Set<TEntity>().AsQueryable(), specification);
        }
    }
}
