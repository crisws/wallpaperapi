using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using wallpaperapi.Data;
using wallpaperapi.Data.Entity;
using wallpaperapi.Models.Request;
using wallpaperapi.Models.Response;

namespace wallpaperapi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly int TokenMinutes;
        private readonly string Secret;
        private readonly string User;
        private readonly string Password;


        public AuthenticationController(WallpaperDbContext context, IConfiguration configuration)
        {

            var serverConf = configuration.GetSection("ServerConfiguration");
            TokenMinutes = int.Parse(serverConf["TokenMinutes"]);
            Secret = serverConf["Secret"];

            User = serverConf["User"]; ;
            Password = serverConf["Password"];
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public ActionResult GetToken([FromBody] UserLoginModelRequest login)
        {


            if (login == null)
            {
                return Ok(new BaseResponse { Message = "Usuario o contraseña no validos", Error = true });
            }

            if (!login.UserName.Equals(User))
            {
                return Ok(new BaseResponse { Message = "Usuario o contraseña no validos", Error = true });
            }

            if (!login.Password.Equals(Password))
            {
                return Ok(new BaseResponse { Message = "Usuario o contraseña no validos", Error = true });
            }


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "api",
                Audience = "api-audience",
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", "1"),
                    new Claim("Role", "Admin" ),
                    new Claim("Name", "su" ),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(TokenMinutes),
                SigningCredentials = new SigningCredentials(
                    key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret)),
                    algorithm: SecurityAlgorithms.HmacSha256
                )
            };

            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var Token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(Token);


            return Ok(new TokenResponse
            {
                BearerJwtToken = "Bearer " + jwtToken,
                JwtToken = jwtToken
            });

        }


    }

    
}
