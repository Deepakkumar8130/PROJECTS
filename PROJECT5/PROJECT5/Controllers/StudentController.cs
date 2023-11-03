using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROJECT5.Data;
using PROJECT5.Models;

namespace Project5.Controllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext _context { get; }
        public StudentController(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public IActionResult Index()
        {
            
            return View();
        }
        
        public IActionResult GetStudents()
        {

            var Student = _context.Students.ToList();
            return Json(Student);
        }




		[HttpPost]
		public IActionResult Create(Student model)
		{
			if (ModelState.IsValid)
			{
				if (_context.Students.Any(e => e.Email == model.Email))
				{
					return Json(new { ok = false, message = "Employee email already registered!" });
				}
				else
				{
					_context.Students.Add(model);
					_context.SaveChanges();
					return Json(new { ok = true, message = "Employee created successfully!" });
				}
			}
			else
			{

				return Json(new { ok = false, message = "something went wrong!" });
			}
		}


		public IActionResult GetStudent(int id)
		{
			var student = _context.Students.FirstOrDefault(e => e.Id == id);
			return Json(student);
		}

		[HttpPost]
		public IActionResult Update(Student model)
		{
			if (ModelState.IsValid)
			{
				_context.Students.Update(model);
				_context.SaveChanges();
				return Json(new { ok = true, message = "Employee Updated successfully!" });
			}
			else
			{

				return Json(new { ok = false, message = "something went wrong!" });
			}
		}


		public IActionResult Delete(int id)
		{
			var emp = _context.Students.FirstOrDefault(e => e.Id == id);
			if (emp != null)
			{
				_context.Students.Remove(emp);
				_context.SaveChanges();
				return Json(new { ok = true, message = "customer Delete successfully!" });
			}
			else
			{
				return Json(new { ok = false, message = "customer not deleted successfully!" });
			}
		}

	}
}
