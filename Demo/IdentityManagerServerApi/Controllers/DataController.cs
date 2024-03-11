using IdentityManagerServerApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedClassLibrary.Contracts;
using SharedClassLibrary.DTOs;

namespace IdentityManagerServerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController(AppDbContext _context) : ControllerBase
    {


        [HttpGet("GetDataByAdmin")]
        [Authorize(Roles ="Admin")]
        public IActionResult GetDataByAdmin()
        {
            var response = _context.Users.ToList();
            return Ok(response);
        }



        [HttpPost("GetDataByUser")]
        [Authorize(Roles = "User")]
        public IActionResult GetDataByUser()
        {
            var response = _context.Users.ToList();
            return Ok(response);
        }
    }
}

