using Microsoft.AspNetCore.Mvc;
using wallpaperapi.Data;
using wallpaperapi.Data.Entity;
using wallpaperapi.Models.Request;
using wallpaperapi.Repository;

namespace wallpaperapi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] CategoryRequest category)
        {
            return Ok(await _categoryRepository.AddAsync(category));
        }


        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(_categoryRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category != null)
            {
                return Ok(category);
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            await _categoryRepository.RemoveByIdAsync(id);
            return Ok();
        }


    }
}
