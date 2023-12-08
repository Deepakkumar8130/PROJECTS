using Azure;
using BAL.Implementations;
using BAL.Interfaces;
using ClaimAPI.Enums;
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


        [HttpGet]
        [Route("GetAllPrograms")]
        public async Task<IActionResult> GetAllPrograms()
        {
            try
            {
                var result = await _programService.GetData();

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
        [Route("GetProgById")]
        public async Task<IActionResult> GetProgById(int ProgId)
        {
            try
            {
                var result = await _programService.GetSingleProg(ProgId);

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
        [Route("RegisteredProgram")]
        public async Task<IActionResult> RegisteredProgram()
        {
            try
            {
                var formData = Request.Form["Program"];
                var program = JsonConvert.DeserializeObject<ProgramMaster>(formData);
                var result = await _programService.AddProg(program);

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
        [Route("UpdateProgram")]
        public async Task<IActionResult> UpdateProgram()
        {
            try
            {
                var formData = Request.Form["Program"];
                var program = JsonConvert.DeserializeObject<ProgramMaster>(formData);
                var result = await _programService.UpdateProg(program);

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
