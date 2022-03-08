using SKINET.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKINET.Business.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetProductBrand(int id);
        Task<IEnumerable<Product>> GetProductsByBrand(int brandId);
        Task<IEnumerable<Product>> GetProductsBrands();
        Task<Product> GetProductType(int id);
        Task<IEnumerable<Product>> GetProductsByType(int typeId);
        Task<IEnumerable<Product>> GetProductsTypes();
        Task<IEnumerable<Product>> GetProductsBrandsTypes();
    }
}
