namespace wallpaperapi.Models.Response
{
    public class TokenResponse
    {
        public string BearerJwtToken { get; set; }
        public string JwtToken { get; set; }
        public string JwtTokenRefresh { get; set; }
    }
}
