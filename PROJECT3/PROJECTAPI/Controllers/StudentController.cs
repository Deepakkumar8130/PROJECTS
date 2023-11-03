using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PROJECTAPI.Models;
using PROJECTAPI.Services;

namespace PROJECTAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private APIResponse response = new APIResponse();

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [Route("GetStudents")]
        public IActionResult GetStudents()
        {
            var students = _studentService.GetStudents();
            if(students.Count > 0)
            {
                response.message = "Students Found";
                response.data = students;
                response.status = StatusCodes.Status200OK;
                response.ok = true;
            }
            else
            {
                response.message = "Students Not Found";
                response.data = null;
                response.status = StatusCodes.Status404NotFound;
                response.ok = false;
            }
            return Ok(response);
        }


        [Route("GetStudentById")]
        public IActionResult GetStudentById(int id)
        {
            var student = _studentService.GetStudentById(id);
            if (student != null)
            {
                response.message = "Student Found";
                response.data = student;
                response.status = StatusCodes.Status200OK;
                response.ok = true;
            }
            else
            {
                response.message = "Student Not Found";
                response.data = null;
                response.status = StatusCodes.Status404NotFound;
                response.ok = false;
            }
            return Ok(response);
        }

        [Route("CreateStudent")]
        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            var result = _studentService.AddStudent(student);
            if (result == "ok")
            {
                response.message = "Student Created Successfully";
                response.data = student;
                response.status = StatusCodes.Status200OK;
                response.ok = true;
            }
            else
            {
                response.message = "Student Not Created";
                response.data = null;
                response.status = StatusCodes.Status500InternalServerError;
                response.ok = false;
            }
            return Ok(response);
        }

        [Route("DeleteStudent")]
        [HttpDelete]
        public IActionResult DeleteStudent(int id)
        {
            var result = _studentService.DeleteStudent(id);
            if (result == "ok")
            {
                response.message = "Student Deleted Successfully";
                response.data = null;
                response.status = StatusCodes.Status200OK;
                response.ok = true;
            }
            else
            {
                response.message = "Student Not Deleted";
                response.data = null;
                response.status = StatusCodes.Status404NotFound;
                response.ok = false;
            }
            return Ok(response);
        }

        [Route("UpdateStudent")]
        [HttpPost]
        public IActionResult UpdateStudent(Student student)
        {
            var result = _studentService.UpdateStudent(student);
            if (result == "ok")
            {
                response.message = "Student Updated Successfully";
                response.data = student;
                response.status = StatusCodes.Status200OK;
                response.ok = true;
            }
            else
            {
                response.message = "Student Not Updated";
                response.data = null;
                response.status = StatusCodes.Status404NotFound;
                response.ok = false;
            }
            return Ok(response);
        }
    }
}
