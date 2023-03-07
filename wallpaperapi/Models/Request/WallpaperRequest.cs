using System.ComponentModel.DataAnnotations;
using wallpaperapi.Utils.ValidationAttributes;

namespace wallpaperapi.Models.Request
{
    public class WallpaperRequest
    {

        [Required]
        public string Name { get; set; }

        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile Image { get; set; }


        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile ThumbnailMobileImage { get; set; }
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile ThumbnailDesktopImage { get; set; }
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile WallpaperMobileImage { get; set; }
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile WallpaperDesktopImage { get; set; }

        public long CategoryId { get; set; }
    }
}
