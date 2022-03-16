using SKINET.Business.Interfaces;
using SKINET.Business.Models;
using SKINET.Data.Context;

namespace SKINET.Data.Repositories
{
    public class ProductTypeRepository : Repository<ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(StoreContext dbContext) : base(dbContext)
        {
        }
    }
}
