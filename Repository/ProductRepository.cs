using BlazorEcom.Data;
using BlazorEcom.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BlazorEcom.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _db.Product.AddAsync(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Product? product = await _db.Product.FirstOrDefaultAsync(c => c.Id == id);

            if (product == null)
            {
                return false;
            }

            _db.Product.Remove(product);
            return (await _db.SaveChangesAsync()) > 0; 
        }

        public async Task<Product> GetAsync(int id)
        {
            Product? product = await _db.Product.FirstOrDefaultAsync(c => c.Id == id);
            
            if (product == null)
            {
                return new Product();
            }

            return product;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _db.Product.Include(p => p.Category).ToListAsync();
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            Product? existingProduct = await _db.Product.FirstOrDefaultAsync(c => c.Id == product.Id);

            if (existingProduct == null)
            {
                return new Product();
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.ImageUrl = product.ImageUrl;
            existingProduct.Price = product.Price;
            _db.Update(existingProduct);
            await _db.SaveChangesAsync();
            return existingProduct;
        }
    }
}
