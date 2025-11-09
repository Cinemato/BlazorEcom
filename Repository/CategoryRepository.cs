using BlazorEcom.Data;
using BlazorEcom.Repository.IRepository;

namespace BlazorEcom.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Category Create(Category category)
        {
            _db.Category.Add(category);
            _db.SaveChanges();
            return category;
        }

        public bool Delete(int id)
        {
            Category? category = _db.Category.Find(id);

            if (category == null)
            {
                return false;
            }

            _db.Category.Remove(category);
            return _db.SaveChanges() > 0; 
        }

        public Category Get(int id)
        {
            Category? category = _db.Category.Find(id);
            
            if (category == null)
            {
                return new Category();
            }

            return category;
        }

        public IEnumerable<Category> GetAll()
        {
            return _db.Category.ToList();
        }

        public Category Update(Category category)
        {
            Category? existingCategory = _db.Category.Find(category.Id);

            if (existingCategory == null)
            {
                return new Category();
            }

            existingCategory.Name = category.Name;
            _db.Update(existingCategory);
            _db.SaveChanges();
            return existingCategory;
        }
    }
}
