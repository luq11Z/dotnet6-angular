using SKINET.Business.Interfaces;
using SKINET.Business.Models;
using SKINET.Data.Context;

namespace SKINET.Data.Repositories
{
    public class ProductBrandRepository : Repository<ProductBrand>, IProductBrandRepository
    {
        public ProductBrandRepository(StoreContext dbContext) : base(dbContext)
        {
        }
    }
}
