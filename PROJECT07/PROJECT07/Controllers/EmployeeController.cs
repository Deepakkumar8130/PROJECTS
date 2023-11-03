using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using PROJECT07.Data;
using PROJECT07.Models;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace PROJECT07.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment { get; }
        public EmployeeController(ApplicationDbContext db, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _db = db;
            Environment = environment;
        }

        public IActionResult getCountry()
        {
            var countries = _db.countries.ToList();
            return Json(countries);
        }
        
        public IActionResult getState(int id)
        {
            var states = _db.states.Where(s =>s.cid== id).ToList();
            return Json(states);
        }
        
        public IActionResult getCity(int id)
        {
            var cities = _db.cities.Where(c =>c.sid==id).ToList();
            return Json(cities);
        }

        public IActionResult Index()
        {
            var rootpath = Environment.WebRootPath;
            //var emplyees = (from e in _db.employees
            //                join cn in _db.countries
            //                on e.country equals cn.id
            //                join s in _db.states
            //                on e.state equals s.id
            //                join cty in _db.cities
            //                on e.city equals cty.id
            //                select new EmployeeVM()
            //                {
            //                    id = e.id,
            //                    profileImg = rootpath + e.profileImg,
            //                    name = e.name,
            //                    age = e.age,
            //                    gender = e.gender,
            //                    country = cn.name,
            //                    state = s.name,
            //                    city = cty.name
            //                }).ToList();

            var emp1 = new EmployeeVM
            {
                name = "Deepak",
                profileImg = "img.jfif",
                age = "20",
                gender = "Male",
                country = "India",
                state = "UP",
                city = "Ghaziabad"
            };
            var emp2 = new EmployeeVM
            {
                name = "Deepak",
                profileImg = "img.jfif",
                age = "20",
                gender = "Male",
                country = "India",
                state = "UP",
                city = "Ghaziabad"
            };
            IEnumerable<EmployeeVM> emp = new EmployeeVM[] { emp1, emp2 };
            return View(emp);
        }

        public IActionResult Create(Employee modal, IFormFile profileImg)
        {
            var rootPath = Environment.WebRootPath;
            var fullPath = Path.Combine(rootPath, "Image", profileImg.FileName);
            FileStream stream = new FileStream(fullPath, FileMode.Create);
            profileImg.CopyTo(stream);
            modal.profileImg = profileImg.FileName;
            if (ModelState.IsValid)
            {
                _db.Add(modal);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
