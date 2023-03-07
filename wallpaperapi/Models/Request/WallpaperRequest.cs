using System.ComponentModel.DataAnnotations;
using wallpaperapi.Utils.ValidationAttributes;

namespace wallpaperapi.Models.Request
{
    public class WallpaperRequest
    {

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxFileSize(1 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile Image { get; set; }
    }
}
