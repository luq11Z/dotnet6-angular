using SKINET.Business.Models;

namespace SKINET.Business.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductWithBrand(int id);
        Task<IEnumerable<Product>> GetProductsByBrand(int brandId);
        Task<Product> GetProductWithType(int id);
        Task<IEnumerable<Product>> GetProductsByType(int typeId);
        Task<Product> GetProductWithBrandAndType(int id);
        Task<IEnumerable<Product>> GetProductsWithBrands();
        Task<IEnumerable<Product>> GetProductsWithTypes();
        Task<IEnumerable<Product>> GetProductsWithBrandsAndTypes(string sort);

    }
}
