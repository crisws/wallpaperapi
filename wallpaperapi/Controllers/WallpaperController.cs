using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wallpaperapi.Data;
using wallpaperapi.Data.Entity;

namespace wallpaperapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WallpaperController : ControllerBase
    {
        private readonly WallpaperDbContext _context;
        //private readonly ServerConfiguration _ServerConfiguration;

        public WallpaperController(WallpaperDbContext context)
        {
            this._context = context; 
        }

        [HttpPost]
        public async Task<ActionResult> Post(Wallpaper wallpaper)
        {
            wallpaper.AddedDate= DateTime.Now.ToUniversalTime();
            wallpaper.GuidImg = Guid.NewGuid().ToString();
            _context.Add(wallpaper);
            await _context.SaveChangesAsync();
            return Ok();
        }




    }
}
