using Microsoft.EntityFrameworkCore;
using wallpaperapi.Data.Entity;

namespace wallpaperapi.Data
{
    public class WallpaperDbContext : DbContext
    {
        public WallpaperDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Wallpaper> Wallpapers => Set<Wallpaper>();
        public DbSet<Category> Categorys => Set<Category>();


        
    }
}
