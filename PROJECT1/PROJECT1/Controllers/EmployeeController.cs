using Microsoft.AspNetCore.Mvc;
using PROJECT1.Data;
using PROJECT1.Models;

namespace PROJECT1.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment { get; }

        public EmployeeController(ApplicationDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            Environment = environment;  
        }

        public JsonResult Country()
        {
            var cnt = _context.countries.ToList();
            return new JsonResult(cnt);
        }

        public JsonResult State(int id)
        {
            var st = _context.states.Where(e => e.country_id == id).ToList();
            return new JsonResult(st);
        }

        public JsonResult City(int id)
        {
            var ct = _context.cities.Where(e => e.state_id == id).ToList();
            return new JsonResult(ct);
        }



        public IActionResult Index()
        {
            var data = (from e in _context.employees
                        join cn in _context.countries
                        on e.country_id equals cn.Id
                        join st in _context.states
                        on e.state_id equals st.Id
                        join cty in _context.cities
                        on e.city_id equals cty.Id
                        select new EmployeeVM()
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Email = e.Email,
                            Mobile = e.Mobile,
                            Gender = e.Gender,
                            country = cn.Name,
                            state = st.Name,
                            city = cty.Name,
                        }).ToList();

            return View(data);
        }



        [HttpPost]
        public IActionResult Create(Employee model)
        {
            if (ModelState.IsValid)
            {
                if(_context.employees.Any(e=>e.Email == model.Email))
                {
                    return Json(new { ok = false, message = "Employee email already registered!" });
                }
                else
                {
                   _context.employees.Add(model);
                    _context.SaveChanges();
                    return Json(new { ok = true, message = "Employee created successfully!" });
                }
            }
            else
            {

                return Json(new { ok = false, message = "something went wrong!" });
            }
        }


        public IActionResult GetEmployee(int id)
        {
            var emp = _context.employees.FirstOrDefault(e => e.Id == id);
            return Json(emp);
        }

        [HttpPost]
        public IActionResult Update(Employee model)
        {
            if (ModelState.IsValid)
            {
                _context.employees.Update(model);
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
            var emp = _context.employees.FirstOrDefault(e => e.Id == id);
            if (emp != null)
            {
                _context.employees.Remove(emp);
                _context.SaveChanges();
                return Json(new { ok = true, message = "customer Delete successfully!" });
            }
            else
            {
                return Json(new { ok = false, message = "customer not deleted successfully!" });
            }
        }


        [HttpPost]
        public IActionResult UpdateProfileImage()
        {
           var id = Request.Form["id"];
            var file = Request.Form.Files[0];
            var rootpath = Environment.WebRootPath;
            var fullPath = Path.Combine(rootpath, "Images", file.FileName);
            FileStream stream = new FileStream(fullPath, FileMode.Create);
            file.CopyTo(stream);

            Image image = new Image()
            {
                Eid=Convert.ToInt32(id),
                path = "Images/" + file.FileName,
            };
            _context.images.Add(image);
            _context.SaveChanges();
            return Json(new { ok = true, message = "image uploaded successfully !" });
        }
    }
}






