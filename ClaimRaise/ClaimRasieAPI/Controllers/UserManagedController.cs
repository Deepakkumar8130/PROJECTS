using BAL.Implementations;
using BAL.Interfaces;
using ClaimAPI.Models;
using MAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClaimRasieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagedController : ControllerBase
    {
        private ICRUD<UserServiceModel> _userService { get; set; }
        private APIResponse response = new APIResponse();

        public UserManagedController(ICRUD<UserServiceModel> userService)
        {
            _userService = userService;
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
        public async Task<IActionResult> RegisteredUser(UserServiceModel user)
        {
            try
            {
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
        public async Task<IActionResult> UpdateUser(UserServiceModel user)
        {
            try
            {
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
    }
}
