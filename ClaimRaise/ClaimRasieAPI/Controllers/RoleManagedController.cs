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
    public class RoleManagedController : ControllerBase
    {
        private IRole _roleService { get; set; }
        private IEntity _entity { get; set; }
        private APIResponse response = new APIResponse();

        public RoleManagedController(IRole roleService)
        {
            _roleService = roleService;
        }


        [HttpGet]
        [Route("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var result = await _roleService.GetData();

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
        [Route("GetRoleById")]
        public async Task<IActionResult> GetRoleById(int RoleId)
        {
            try
            {
                var result = await _roleService.GetSingleRole(RoleId);

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
        [Route("RegisteredRole")]
        public async Task<IActionResult> AddRole()
        {
            try
            {
                var formData = Request.Form["Role"];
                var role = JsonConvert.DeserializeObject<Role>(formData);
                var result = await _roleService.AddData(role);

                if (string.IsNullOrEmpty(result))
                {
                    response.status = 200;
                    response.ok = true;
                    response.data = null;
                    response.message = "Role Registered successfully!";
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
        [Route("UpdateRole")]
        public async Task<IActionResult> UpdateUser()
        {
            try
            {
                var formData = Request.Form["Role"];
                var role = JsonConvert.DeserializeObject<Role>(formData);
                var result = await _roleService.UpdateData(role);

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
        [Route("GetActiveRoles")]
        public async Task<IActionResult> GetActiveRoles()
        {
            try
            {
                var result = await _roleService.GetActiveRoles();

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


        [HttpPost]
        [Route("AssignedRole")]
        public async Task<IActionResult> AssignedRole(RoleAssginedModel model)
        {
            try
            {
                var formData = Request.Form["Role"];
                var role = JsonConvert.DeserializeObject<Role>(formData);
                var result = await _roleService.AssignedRole(model);

                if (string.IsNullOrEmpty(result))
                {
                    response.status = 200;
                    response.ok = true;
                    response.data = null;
                    response.message = "Role Assigned successfully!";
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
