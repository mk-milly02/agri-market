using AgriMarket.API.Data;
using AgriMarket.API.Models.Domain.Products;
using AgriMarket.API.Repositories.Products.Interfaces;

namespace AgriMarket.API.Repositories.Products.Implementations
{
    public class ProductsRepository(ApplicationDBContext db) : IProductRepository
    {
        private readonly ApplicationDBContext db = db;

        public async Task<Product> CreateAsync(Product product)
        {
            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();
            return product;
        }
    }
}