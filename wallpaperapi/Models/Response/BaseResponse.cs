namespace wallpaperapi.Models.Response
{
    public class BaseResponse
    {
        public int Code { get; set; } = 200;
        public string Message { get; set; } = "";
        public bool Error { get; set; } = false;

    }
}
