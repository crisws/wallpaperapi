using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using wallpaperapi.Data;
using wallpaperapi.Models.Request;
using wallpaperapi.Repository;

namespace wallpaperapi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class WallpaperController : ControllerBase
    {
        private readonly WallpaperDbContext _context;
        private readonly IWallpaperRepository _wallpaperRepository;
        //private readonly ServerConfiguration _ServerConfiguration;

        public WallpaperController(WallpaperDbContext context, IWallpaperRepository wallpaperrepository)
        {
            _context = context; 
            _wallpaperRepository = wallpaperrepository;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm]WallpaperRequest wallpaper)
        {
            return Ok(await _wallpaperRepository.AddAsync(wallpaper));
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(_wallpaperRepository.GetAll());
        }



        [HttpGet("{CategoryId}")]
        public async Task<ActionResult> GetAllByCategoryId(long CategoryId)
        {
            return Ok(_wallpaperRepository.GetAllByCategoryId(CategoryId));
        }

    }
}
