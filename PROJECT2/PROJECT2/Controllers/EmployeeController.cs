using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PROJECT2.Data;
using PROJECT2.Models;

namespace Project2.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private ApplicationDbContext DbContext { get; }
        public EmployeeController(ApplicationDbContext DbContext)
        {
            this.DbContext = DbContext;
        }
        public IActionResult Index()
        {
            var employee = (from e in DbContext.Employee
                            join d in DbContext.Department
                            on e.Department equals d.DeptId
                            join g in DbContext.Gender
                            on e.Gender equals g.genderId
                            select new EmployeeVM()

                            {
                                Id = e.Id,
                                Name = e.Name,
                                Phone =e.Phone,
                                Email = e.Email,
                                Gender = g.gender,
                                Salary = (double)e.Salary,
                                Department = d.Name
                            }).ToList();
            return View(employee);
        }


        public IActionResult CreateOrEdit(int Id = 0)
        {
            if(Id == 0)
            {
				ViewBag.Department = DbContext.Department.ToList();
                ViewBag.Gender = DbContext.Gender.ToList();
                return View(new Employee());
            }
            else
            {
                Employee? employee = DbContext.Employee.FirstOrDefault(e=>e.Id==Id);
                if(employee == null)
                {
                    return NotFound();
                }
                var Department = DbContext.Department.ToList();
                ViewBag.Department = Department;
                var Gender = DbContext.Gender.ToList();
                ViewBag.Gender = Gender;
                return View(employee);
            }
        }

        [HttpPost]
        public IActionResult CreateOrEdit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                if(employee.Id == 0)
                {

                    if(DbContext.Employee.Any(e=>e.Email == employee.Email))
                    {

                        ModelState.AddModelError("Email", "Email Already Exist");
                        ViewBag.Department = DbContext.Department.ToList();
                        ViewBag.Gender = DbContext.Gender.ToList();
                        return View(employee);

                    }
                    DbContext.Employee.Add(employee);
                    DbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                DbContext.Employee.Update(employee);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                var Department = DbContext.Department.ToList();
                ViewBag.Department = Department;
                var Gender = DbContext.Gender.ToList();
                ViewBag.Gender = Gender;
                return View(employee);
            }
        }

        public IActionResult Delete(int? id)
        {
            Employee? employee = DbContext.Employee.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewBag.Department = DbContext.Department.ToList();
            ViewBag.Gender = DbContext.Gender.ToList();
            return View(employee);
        }

        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            DbContext.Employee.Remove(employee);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
