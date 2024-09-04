using AgriMarket.API.Models.Domain.Products;

namespace AgriMarket.API.Repositories.Products.Interfaces
{
    public interface IProductRepository
    {
       Task<Product> CreateAsync(Product product);
    }
}