using System.ComponentModel.DataAnnotations;

namespace wallpaperapi.Models.Request
{
    public class CategoryRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
