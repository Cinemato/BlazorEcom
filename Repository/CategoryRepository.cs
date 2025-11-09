using BlazorEcom.Data;
using BlazorEcom.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BlazorEcom.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await _db.Category.AddAsync(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Category? category = await _db.Category.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return false;
            }

            _db.Category.Remove(category);
            return (await _db.SaveChangesAsync()) > 0; 
        }

        public async Task<Category> GetAsync(int id)
        {
            Category? category = await _db.Category.FirstOrDefaultAsync(c => c.Id == id);
            
            if (category == null)
            {
                return new Category();
            }

            return category;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _db.Category.ToListAsync();
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            Category? existingCategory = await _db.Category.FirstOrDefaultAsync(c => c.Id == category.Id);

            if (existingCategory == null)
            {
                return new Category();
            }

            existingCategory.Name = category.Name;
            _db.Update(existingCategory);
            await _db.SaveChangesAsync();
            return existingCategory;
        }
    }
}
