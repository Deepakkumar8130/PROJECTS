using Azure;
using BAL.Implementations;
using BAL.Interfaces;
using ClaimAPI.Enums;
using ClaimAPI.Models;
using MAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClaimRasieAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        private IProgramService _programService { get; set; }
        private APIResponse response = new APIResponse();

        public ProgramController(IProgramService programService)
        {
            _programService = programService;
        }



        [HttpGet]
        [Route("GetUserByEmailId")]
        public async Task<IActionResult> GetUserByEmailId(string emailId)
        {
            try
            {
                var userInfo = await _programService.GetUserByEmailId(emailId);
                response.status = 200;
                response.ok = true;
                response.data = userInfo;
                response.message = "Get User info successfully!";
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


        [HttpGet]
        [Route("GetProgramsById")]
        public async Task<IActionResult> GetProgramsById(int UserId)
        {
            try
            {
                var Programs = await _programService.GetProgramsById(UserId);
                response.status = 200;
                response.ok = true;
                response.data = Programs;
                response.message = "Get User Programs successfully!";
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
    }
}
