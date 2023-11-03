using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PROJECTAPI.Models;
using PROJECTAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PROJECTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly APIResponse response = new APIResponse();
        private IConfiguration configuration;

        public AccountController(IUserService userService, IConfiguration config)
        {
            this.userService = userService;
            this.configuration = config;
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login(LoginModel user)
        {
            var result = userService.Login(user);
            if(result != null)
            {
                response.status = StatusCodes.Status200OK;
                response.message = "User Authenticated Successfully";
                response.ok = true;
                response.data = result;
                response.token = GenerateJSONWebToken(result);

            }
            else
            {
                response.status = StatusCodes.Status101SwitchingProtocols;
                response.message = "User Not Authenticated";
                response.ok = false;
                response.data = null;
            }
            return Ok(response);
        }

        [Route("Register")]
        [HttpPost]
        public IActionResult Register(UserModel user)
        {
            var result = userService.SignUp(user);
            if(result == "ok")
            {
                response.status = StatusCodes.Status200OK;
                response.message = "User Registered Successfully";
                response.ok = true;
                response.data = user;

            }
            else
            {
                response.status = StatusCodes.Status101SwitchingProtocols;
                response.message = "User Not Registered";
                response.ok = false;
                response.data = null;
            }
            return Ok(response);
        }


        private string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, userInfo.Email),
            new Claim(JwtRegisteredClaimNames.Email, userInfo.UserId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
              };

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
