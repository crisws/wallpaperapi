using Microsoft.AspNetCore.Mvc;
using wallpaperapi.Models.Response;

namespace wallpaperapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : Controller
    {
        public readonly string _version;
        public readonly string _enviroment;


        public DefaultController(IConfiguration configuration) {

            var serverConf = configuration.GetSection("ServerConfiguration");
            _version = serverConf["Version"];
            _enviroment = serverConf["Enviroment"];
        }



        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new BaseResponse { Message = "Server running on version: " + _version + ", Enviroment: " + _enviroment });
        }
    }
}
