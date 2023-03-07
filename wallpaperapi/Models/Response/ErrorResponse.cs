namespace wallpaperapi.Models.Response
{
    public class ErrorResponse : BaseResponse
    {
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public string Data { get; set; }
    }
}
