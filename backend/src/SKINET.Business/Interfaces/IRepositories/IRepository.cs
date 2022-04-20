using SKINET.Business.Models;

namespace SKINET.Business.Interfaces
{

    /* Provides all necessary methods to any entity.
       TEntity can only be applied to Entity childs. This way it is not possible passing any type of objects.
       In other words, only objects that implements Entity can use this interface.
    */
    public interface IRepository<TEntity> where TEntity : Entity
    {
        /* Tasks makes better usability, performace and helth to the system. */
        void Add(TEntity entity);
        Task<TEntity> GetById(int id);
        Task<List<TEntity>> GetAll();
        void Update(TEntity entity);
        void Delete(TEntity entity);

        /* Find by linq/lambda expression*/
        Task<TEntity> GetWithSpec(ISpecification<TEntity> specification);
        Task<List<TEntity>> GetAllWithSpec(ISpecification<TEntity> specification);
        Task<int> Count(ISpecification<TEntity> specification);
        Task<int> SaveChanges();
    }
}
