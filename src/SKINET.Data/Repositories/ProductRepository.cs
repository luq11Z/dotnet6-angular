using Microsoft.EntityFrameworkCore;
using SKINET.Business.Interfaces;
using SKINET.Business.Models;
using SKINET.Data.Context;

namespace SKINET.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        public ProductRepository(StoreContext dbContext) : base(dbContext)
        {  
        }

        public async Task<Product> GetProductBrand(int id)
        {
            return await dbContext.Products.AsNoTracking().Include(p => p.ProductBrand).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsByBrand(int brandId)
        {
            return await Find(p => p.ProductBrandId == brandId);
        }

        public async Task<IEnumerable<Product>> GetProductsBrands()
        {
            return await dbContext.Products.AsNoTracking().Include(p => p.ProductBrand).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Product> GetProductType(int id)
        {
            return await dbContext.Products.AsNoTracking().Include(p => p.ProductType).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsByType(int typeId)
        {
            return await Find(p => p.ProductTypeId == typeId);
        }

        public async Task<IEnumerable<Product>> GetProductsTypes()
        {
            return await dbContext.Products.AsNoTracking().Include(p => p.ProductType).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsBrandsTypes()
        {
            return await dbContext.Products.AsNoTracking().Include(p => p.ProductType).Include(p => p.ProductBrand).OrderBy(x => x.Name).ToListAsync();
        }
    }
}
