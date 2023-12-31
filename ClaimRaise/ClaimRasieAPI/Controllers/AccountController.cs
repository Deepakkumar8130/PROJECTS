﻿using BAL.Interfaces;
using ClaimAPI.Enums;
using ClaimAPI.Models;
using MAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClaimRasieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;
        private IProgramService _programService;
        private APIResponse response = new APIResponse();
        private IConfiguration _config;

        public AccountController(IAccountService accountService, IConfiguration config, IProgramService programService)
        {
            _accountService = accountService;
            this._config = config;
            _programService = programService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserLogin user)
        {
            try
            {
                var result = await _accountService.Login(user);
                if (result == LoginResult.Success)
                {
                    var userInfo = await _programService.GetUserByEmailId(user.UserId);
                    response.status = 200;
                    response.ok = true;
                    response.data = userInfo;
                    response.message = "User authenticated successfully!";
                    response.token = GenerateJSONWebToken(userInfo);

                }
                else if (result == LoginResult.Invalid)
                {
                    response.status = 101;
                    response.ok = false;
                    response.data = null;
                    response.message = "User not autheticated!";
                }
                else if (result == LoginResult.NotExist)
                {
                    response.status = 101;
                    response.ok = false;
                    response.data = null;
                    response.message = "User not exist!";
                }
                else
                {
                    response.status = 101;
                    response.ok = false;
                    response.data = null;
                    response.message = "Something went wrong!";
                }
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.ok = false;
                response.data = null;
                response.message = ex.Message;
            }

            return Ok(response);
        }



        private string GenerateJSONWebToken(UserVM userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, userInfo.Email),
            new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
              };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpGet]
        [Route("Auth")]
        public async Task<IActionResult> Auth(int UserId, string path)
        {
            try
            {

                var result = await _accountService.pageRights(UserId, path);
                if (string.IsNullOrEmpty(result.Item1))
                {
                    if (result.Item2 == "1")
                    {
                        response.status = 200;
                        response.ok = true;
                        response.data = null;
                        response.message = "Approved";
                    }
                    else
                    {
                        response.status = -100;
                        response.ok = false;
                        response.data = null;
                        response.message = "Dont have permission for this page";

                    }
                }
                else
                {
                    response.status = -100;
                    response.ok = false;
                    response.data = null;
                    response.message = result.Item1;

                }
            }
            catch (Exception ex)
            {
                response.status = -100;
                response.ok = false;
                response.data = null;
                response.message = ex.Message;
            }
            return Ok(response);
        }
    }
}
