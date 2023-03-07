using Microsoft.AspNetCore.Http.HttpResults;
using wallpaperapi.Data;
using wallpaperapi.Data.Entity;
using wallpaperapi.Data.Enums;
using wallpaperapi.Models.Request;
using wallpaperapi.Service;

namespace wallpaperapi.Repository
{
    public class WallpaperRepository : IWallpaperRepository
    {
        private readonly WallpaperDbContext context;
        private readonly IAzureStorageService azureStorageService;
        public WallpaperRepository(WallpaperDbContext context, IAzureStorageService azureStorageService)
        {
            this.context = context;
            this.azureStorageService = azureStorageService;
        }

        public async Task<Wallpaper> AddAsync(WallpaperRequest request)
        {

            var wallpaper = new Wallpaper()
            {
                Name = request.Name,
                AddedDate = DateTime.Now.ToUniversalTime()
            };
            

            if (request.Image != null)
            {
                wallpaper.GuidImg = await this.azureStorageService.UploadAsync(request.Image, ContainerEnum.baseimage);
            }


            this.context.Add(wallpaper);
            this.context.SaveChanges();

            return wallpaper;
        }

        public List<Wallpaper> GetAll()
        {
            return this.context.Wallpapers.ToList();
        }

        public Wallpaper GetById(int id)
        {
            return this.context.Wallpapers.Find(id);
        }

        public async Task RemoveByIdAsync(int id)
        {
            var wallpaper = this.context.Wallpapers.Find(id);
            if (wallpaper != null)
            {
                if (!string.IsNullOrEmpty(wallpaper.GuidImg))
                {
                    await this.azureStorageService.DeleteAsync(ContainerEnum.baseimage, wallpaper.GuidImg);
                }

                this.context.Remove(wallpaper);
                this.context.SaveChanges();
            }
        }
     
    }

    public interface IWallpaperRepository
    {
        List<Wallpaper> GetAll();
        Wallpaper GetById(int id);
        Task<Wallpaper> AddAsync(WallpaperRequest request);
        Task RemoveByIdAsync(int id);
    }
}
