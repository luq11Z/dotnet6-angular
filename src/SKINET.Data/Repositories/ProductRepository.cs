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

        /* Aggregates and search criterias without the generics specifications. */
        public async Task<Product> GetProductWithBrand(int id)
        {
            return await dbContext.Products.AsNoTracking().Include(p => p.ProductBrand).FirstOrDefaultAsync(x => x.Id == id);
        }

        /* Aggregates and search criterias without the generics specifications. */
        public async Task<Product> GetProductWithBrandAndType(int id)
        {
            return await dbContext.Products.AsNoTracking().Include(p => p.ProductBrand).Include(p => p.ProductType).FirstOrDefaultAsync(x => x.Id == id);
        }

        /* Aggregates and search criterias without the generics specifications. */
        public async Task<IEnumerable<Product>> GetProductsByBrand(int brandId)
        {
            return await Find(p => p.ProductBrandId == brandId);
        }

        /* Aggregates and search criterias without the generics specifications. */
        public async Task<IEnumerable<Product>> GetProductsWithBrands()
        {
            return await dbContext.Products.AsNoTracking().Include(p => p.ProductBrand).OrderBy(x => x.Name).ToListAsync();
        }

        /* Aggregates and search criterias without the generics specifications. */
        public async Task<Product> GetProductWithType(int id)
        {
            return await dbContext.Products.AsNoTracking().Include(p => p.ProductType).FirstOrDefaultAsync(x => x.Id == id);
        }

        /* Aggregates and search criterias without the generics specifications. */
        public async Task<IEnumerable<Product>> GetProductsByType(int typeId)
        {
            return await Find(p => p.ProductTypeId == typeId);
        }

        /* Aggregates and search criterias without the generics specifications. */
        public async Task<IEnumerable<Product>> GetProductsWithTypes()
        {
            return await dbContext.Products.AsNoTracking().Include(p => p.ProductType).OrderBy(x => x.Name).ToListAsync();
        }

        /* Aggregates and search criterias without the generics specifications. */
        public async Task<IEnumerable<Product>> GetProductsWithBrandsAndTypes(string sort)
        {
            var products = dbContext.Products.AsNoTracking().Include(p => p.ProductType).Include(p => p.ProductBrand).OrderBy(x => x.Name);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        products = products.OrderBy(x => x.Price);
                        break;
                    case "priceDesc":
                        products = products.OrderByDescending(x => x.Price);
                        break;
                    default:   
                        products = products.OrderBy(x => x.Name);
                        break;
                }
            }

            return await products.ToListAsync();
        }
    }
}
