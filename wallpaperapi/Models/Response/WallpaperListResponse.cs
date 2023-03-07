using wallpaperapi.Data.Entity;

namespace wallpaperapi.Models.Response
{
    public class WallpaperListResponse : BaseResponse
    {
        public List<WallpaperForListResponse> Wallpapers { get; set; }
    }

    public class WallpaperForListResponse
    {
        public long Id { get; set; }
        public DateTime AddedDate { get; set; }
        public string Name { get; set; }
        public string ThumbnailMobileLink { get; set; }
        public string ThumbnailDesktopLink { get; set; }
        public string WallpaperMobileLink { get; set; }
        public string WallpaperDesktopLink { get; set; }
        public string Category { get; set; }

        public long CategoryId { get; set; }

    }
}
