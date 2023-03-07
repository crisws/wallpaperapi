using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace wallpaperapi.Data.Entity
{
    public class Category
    {
        [Key]
        public long Id { get; set; }

        public DateTime AddedDate { get; set; }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public List<Wallpaper> Wallpapers { get; set; }
    }
}
