using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace wallpaperapi.Data.Entity
{
    public class Wallpaper
    {
        [Key]
        public long Id { get; set; }

        public DateTime AddedDate { get; set; }

        public string? GuidImg { get; set; }

        [Required]
        public string Name { get; set; }

        public string ThumbnailMobileLink { get; set; }
        public string ThumbnailDesktopLink { get; set; }
        public string WallpaperMobileLink { get; set; }
        public string WallpaperDesktopLink { get; set; }

        [ForeignKey("Category")]
        public long CategoryId { get; set; }



    }
}
