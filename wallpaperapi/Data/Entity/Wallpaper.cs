using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace wallpaperapi.Data.Entity
{
    public class Wallpaper
    {
        [Key]
        [JsonIgnore]
        public long Id { get; set; }

        [JsonIgnore]
        public DateTime AddedDate { get; set; }

        [JsonIgnore]
        public string? GuidImg { get; set; }

        [Required]
        public string? Name { get; set; }



    }
}
