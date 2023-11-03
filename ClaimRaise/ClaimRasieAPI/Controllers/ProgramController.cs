using Azure;
using BAL.Implementations;
using BAL.Interfaces;
using ClaimAPI.Enums;
using ClaimAPI.Models;
using MAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClaimRasieAPI.Controllers
{
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



        [HttpPost]
        [Route("GetUserByEmailId")]
        public async Task<UserVM> GetUserByEmailId(string emailID)
        {
            try
            {
                var userInfo = await _programService.GetUserByEmailId(emailID);
                return userInfo;
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPost]
        [Route("GetProgramsById")]
        public async Task<List<UserProgram>> GetProgramsById(int UserId)
        {
            var Programs = await _programService.GetProgramsById(UserId);
            return Programs;
        }
    }
}
