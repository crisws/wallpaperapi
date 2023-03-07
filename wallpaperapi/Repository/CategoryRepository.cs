using wallpaperapi.Data;
using wallpaperapi.Data.Entity;
using wallpaperapi.Data.Enums;
using wallpaperapi.Models.Request;
using wallpaperapi.Service;

namespace wallpaperapi.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly WallpaperDbContext _context;
        public CategoryRepository(WallpaperDbContext context)
        {
            _context = context;
        }

        public async Task<Category> AddAsync(CategoryRequest request)
        {
            var category = new Category()
            {
                Name = request.Name,
                AddedDate = DateTime.Now.ToUniversalTime()
            };

            _context.Add(category);
            _context.SaveChanges();

            return category;
        }

        public  List<Category> GetAll()
        {
            return _context.Categorys.ToList();
        }

        public  Category GetById(long id)
        {
            return _context.Categorys.Find(id);
        }

        public async Task RemoveByIdAsync(int id)
        {
            var category = _context.Categorys.Find(id);
            if (category != null)
            {
                _context.Remove(category);
                _context.SaveChanges();
            }
        }
    }

    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category GetById(long id);
        Task<Category> AddAsync(CategoryRequest request);
        Task RemoveByIdAsync(int id);
    }
}
