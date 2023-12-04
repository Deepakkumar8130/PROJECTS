using BAL.Implementations;
using BAL.Interfaces;
using ClaimAPI.Models;
using MAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClaimRasieAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagedController : ControllerBase
    {
        private ICRUD<UserServiceModel> _userService { get; set; }
        private IEntity _entity { get; set; }
        private APIResponse response = new APIResponse();

        public UserManagedController(ICRUD<UserServiceModel> userService, IEntity entity)
        {
            _userService = userService;
            _entity = entity;
        }


        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(int UserId)
        {
            try
            {
                var result = await _userService.GetData(UserId);

                if (string.IsNullOrEmpty(result.Item1))
                {
                    response.status = 200;
                    response.ok = true;
                    response.data = result.Item2;
                    response.message = "All Records get successfully!";
                }
                else
                {
                    response.status = 505;
                    response.ok = false;
                    response.data = null;
                    response.message = result.Item1;
                }
            }
            catch (Exception ex)
            {
                response.status = 505;
                response.ok = false;
                response.data = null;
                response.message = ex.Message;
            }
            return Ok(response);
        }
        
        
        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(int UserId)
        {
            try
            {
                var result = await _userService.GetSingleUSer(UserId);

                if (string.IsNullOrEmpty(result.Item1))
                {
                    response.status = 200;
                    response.ok = true;
                    response.data = result.Item2;
                    response.message = "Record found successfully!";
                }
                else
                {
                    response.status = 505;
                    response.ok = false;
                    response.data = null;
                    response.message = result.Item1;
                }
            }
            catch (Exception ex)
            {
                response.status = 505;
                response.ok = false;
                response.data = null;
                response.message = ex.Message;
            }
            return Ok(response);
        }


        [HttpPost]
        [Route("RegisteredUser")]
        public async Task<IActionResult> RegisteredUser()
        {
            try
            {
                var formData = Request.Form["User"];
                var user = JsonConvert.DeserializeObject<UserServiceModel>(formData);
                var result = await _userService.AddData(user);

                if (string.IsNullOrEmpty(result))
                {
                    response.status = 200;
                    response.ok = true;
                    response.data = null;
                    response.message = "Data Registered successfully!";
                }
                else
                {
                    response.status = 505;
                    response.ok = false;
                    response.data = null;
                    response.message = result;
                }
            }
            catch (Exception ex)
            {
                response.status = 505;
                response.ok = false;
                response.data = null;
                response.message = ex.Message;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser()
        {
            try
            {
                var formData = Request.Form["User"];
                var user = JsonConvert.DeserializeObject<UserServiceModel>(formData);
                var result = await _userService.UpdateData(user);

                if (string.IsNullOrEmpty(result))
                {
                    response.status = 200;
                    response.ok = true;
                    response.data = null;
                    response.message = "Data Saved successfully!";
                }
                else
                {
                    response.status = 505;
                    response.ok = false;
                    response.data = null;
                    response.message = result;
                }
            }
            catch (Exception ex)
            {
                response.status = 505;
                response.ok = false;
                response.data = null;
                response.message = ex.Message;
            }
            return Ok(response);
        }


        [HttpGet]
        [Route("GetUserByRole")]
        public async Task<IActionResult> GetUserByRole(int UserId, string Role)
        {
            try
            {
                var result = await _entity.GetEntities(UserId, Role);

                if (string.IsNullOrEmpty(result.Item1))
                {
                    response.status = 200;
                    response.ok = true;
                    response.data = result.Item2;
                    response.message = "User found successfully!";
                }
                else
                {
                    response.status = 505;
                    response.ok = false;
                    response.data = null;
                    response.message = result.Item1;
                }
            }
            catch (Exception ex)
            {
                response.status = 505;
                response.ok = false;
                response.data = null;
                response.message = ex.Message;
            }
            return Ok(response);
        }


        [HttpGet]
        [Route("GetUsersWithRole")]
        public async Task<IActionResult> GetUsersWithRole()
        {
            try
            {
                var result = await _entity.GetUsersWithRole();

                if (string.IsNullOrEmpty(result.Item1))
                {
                    response.status = 200;
                    response.ok = true;
                    response.data = result.Item2;
                    response.message = "All Records get successfully!";
                }
                else
                {
                    response.status = 505;
                    response.ok = false;
                    response.data = null;
                    response.message = result.Item1;
                }
            }
            catch (Exception ex)
            {
                response.status = 505;
                response.ok = false;
                response.data = null;
                response.message = ex.Message;
            }
            return Ok(response);
        }
    }
}
