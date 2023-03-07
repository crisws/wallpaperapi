using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel;
using wallpaperapi.Data;
using wallpaperapi.Data.Entity;
using wallpaperapi.Data.Enums;
using wallpaperapi.Models.Request;
using wallpaperapi.Models.Response;
using wallpaperapi.Service;

namespace wallpaperapi.Repository
{
    public class WallpaperRepository : IWallpaperRepository
    {
        private readonly WallpaperDbContext _context;

        private readonly IAzureStorageService azureStorageService;
        public WallpaperRepository(WallpaperDbContext context, IAzureStorageService azureStorageService)
        {
            _context = context;
            this.azureStorageService = azureStorageService;

        }

        public async Task<Wallpaper> AddAsync(WallpaperRequest request)
        {

            var wallpaper = new Wallpaper()
            {
                Name = request.Name,
                AddedDate = DateTime.Now.ToUniversalTime(),
                CategoryId = request.CategoryId,
            };
            

            if (request.Image != null)
            {
                wallpaper.GuidImg = await this.azureStorageService.UploadAsync(request.Image, ContainerEnum.baseimage);
            }

            if (request.ThumbnailDesktopImage != null)
            {
                wallpaper.ThumbnailDesktopLink = await this.azureStorageService.UploadAsync(request.ThumbnailDesktopImage, ContainerEnum.thumbnaildesktop);
            }

            if (request.ThumbnailMobileImage != null)
            {
                wallpaper.ThumbnailMobileLink = await this.azureStorageService.UploadAsync(request.ThumbnailMobileImage, ContainerEnum.thumbnailmobile);
            }

            if (request.WallpaperDesktopImage != null)
            {
                wallpaper.WallpaperDesktopLink = await this.azureStorageService.UploadAsync(request.WallpaperDesktopImage, ContainerEnum.wallpaperdesktop);
            }

            if (request.WallpaperMobileImage != null)
            {
                wallpaper.WallpaperMobileLink = await this.azureStorageService.UploadAsync(request.WallpaperMobileImage, ContainerEnum.wallpapermobile);
            }


            _context.Add(wallpaper);
            _context.SaveChanges();

            return wallpaper;
        }

        public WallpaperListResponse GetAll()
        {
            WallpaperListResponse response = new WallpaperListResponse();

            var r = _context.Wallpapers
                .Join(_context.Categorys,
                    w => w.CategoryId,
                    c => c.Id,
                    (w,c) => new WallpaperForListResponse
                    { 
                        Id = w.Id, AddedDate = w.AddedDate, 
                        Category = c.Name,
                        CategoryId = c.Id,
                        Name = w.Name, 
                        ThumbnailDesktopLink = w.ThumbnailDesktopLink,
                        ThumbnailMobileLink = w.ThumbnailMobileLink, 
                        WallpaperDesktopLink= w.WallpaperDesktopLink, 
                        WallpaperMobileLink = w.WallpaperMobileLink
                    })
                .ToList();
            response.Wallpapers= r;

            return response;

        }

        public WallpaperListResponse GetAllByCategoryId(long FCategoryId)
        {
            WallpaperListResponse response = new WallpaperListResponse();

            var r = _context.Wallpapers
                .Join(_context.Categorys,
                    w => w.CategoryId,
                    c => c.Id,
                    (w, c) => new WallpaperForListResponse
                    {
                        Id = w.Id,
                        AddedDate = w.AddedDate,
                        Category = c.Name,
                        CategoryId = c.Id,
                        Name = w.Name,
                        ThumbnailDesktopLink = w.ThumbnailDesktopLink,
                        ThumbnailMobileLink = w.ThumbnailMobileLink,
                        WallpaperDesktopLink = w.WallpaperDesktopLink,
                        WallpaperMobileLink = w.WallpaperMobileLink
                    })
                .Where(x => x.CategoryId == FCategoryId)
                .ToList();
            response.Wallpapers = r;

            return response;

        }

        public Wallpaper GetById(int id)
        {
            return _context.Wallpapers.Find(id);
        }

        public async Task RemoveByIdAsync(int id)
        {
            var wallpaper = _context.Wallpapers.Find(id);
            if (wallpaper != null)
            {
                if (!string.IsNullOrEmpty(wallpaper.GuidImg))
                {
                    await this.azureStorageService.DeleteAsync(ContainerEnum.baseimage, wallpaper.GuidImg);
                }

                _context.Remove(wallpaper);
                _context.SaveChanges();
            }
        }
     
    }

    public interface IWallpaperRepository
    {
        WallpaperListResponse GetAllByCategoryId(long categoryId);
        WallpaperListResponse GetAll();
        Wallpaper GetById(int id);
        Task<Wallpaper> AddAsync(WallpaperRequest request);
        Task RemoveByIdAsync(int id);
    }
}
