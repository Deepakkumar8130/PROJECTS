using BAL.Implementations;
using BAL.Interfaces;
using ClaimAPI.Models;
using MAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ClaimRasieAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramRightsController : ControllerBase
    {
        private IProgramRights _programRights { get; set; }
        private APIResponse response = new APIResponse();

        public ProgramRightsController(IProgramRights programRights)
        {
            _programRights = programRights;
        }

        [HttpGet]
        [Route("GetProgramsRightsByUserId")]
        public async Task<IActionResult> GetProgramsRightsByUserId(int UserId)
        {
            try
            {
                var result = await _programRights.GetProgramsRightsByUserId(UserId);

                if (string.IsNullOrEmpty(result.Item1))
                {
                    response.status = 200;
                    response.ok = true;
                    response.data = result.Item2;
                    response.message = "Program Rights Get Successfully!";
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
        [Route("AssignIndividualProgramsRights")]
        public async Task<IActionResult> AssignIndividualProgramsRights()
        {
            try
            {
                int UserId = Convert.ToInt32(Request.Form["UserId"]);
                string str = Request.Form["lstOfPrograms"];
                var lstPrograms = JsonSerializer.Deserialize<List<AssignProgramRightModel>>(str);
                var xmlPrograms = XMLUtil.GenerateXML(lstPrograms);

                var result = await _programRights.AssignIndividualProgramsRights(UserId, xmlPrograms);

                if (string.IsNullOrEmpty(result.Item1))
                {
                    response.status = 200;
                    response.ok = true;
                    response.data = null;
                    response.message = "Program Rights Get Successfully!";
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
        [Route("GetProgramsRightsByRoleId")]
        public async Task<IActionResult> GetProgramsRightsByRoleId(int RoleId)
        {
            try
            {
                var result = await _programRights.GetProgramsRightsByRoleId(RoleId);

                if (string.IsNullOrEmpty(result.Item1))
                {
                    response.status = 200;
                    response.ok = true;
                    response.data = result.Item2;
                    response.message = "Program Rights Get Successfully!";
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
        [Route("AssignGroupProgramsRights")]
        public async Task<IActionResult> AssignGroupProgramsRights()
        {
            try
            {
                int RoleId = Convert.ToInt32(Request.Form["RoleId"]);
                string str = Request.Form["lstOfPrograms"];
                var lstPrograms = JsonSerializer.Deserialize<List<AssignProgramRightModel>>(str);
                var xmlPrograms = XMLUtil.GenerateXML(lstPrograms);

                var result = await _programRights.AssignGroupProgramsRights(RoleId, xmlPrograms);

                if (string.IsNullOrEmpty(result.Item1))
                {
                    response.status = 200;
                    response.ok = true;
                    response.data = null;
                    response.message = "Program Rights Get Successfully!";
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
