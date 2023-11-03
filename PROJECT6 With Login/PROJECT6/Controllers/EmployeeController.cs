using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROJECT6.Models;
using PROJECT6.Models.VM;

namespace PROJECT6.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment { get; }

        public EmployeeController(ApplicationDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            Environment = environment;
        }

        public JsonResult GetCountry()
        {
            var cnt = _context.countries.ToList();
            return new JsonResult(cnt);
        }

        public JsonResult GetState(int id)
        {
            var st = _context.states.Where(e => e.country_id == id).ToList();
            return new JsonResult(st);
        }

        public JsonResult GetCity(int id)
        {
            var ct = _context.cities.Where(e => e.state_id == id).ToList();
            return new JsonResult(ct);
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult GetEmployees()
        {
            var emp = (from e in _context.employees
                       join cnt in _context.countries on e.Country_Id equals cnt.id
                       join st in _context.states on e.State_Id equals st.id
                       join cty in _context.cities on e.City_Id equals cty.id
                       select new EmployeeVM()
                       {
                           Id = e.Id,
                           ProfileImg = "/Images/" + e.ProfileImg,
                           Name = e.Name,
                           Gender = e.Gender,
                           DateOfBirth = e.DateOfBirth,
                           Email = e.Email,
                           Mobile = e.Mobile,
                           Address = e.Address,
                           Country = cnt.name,
                           State = st.name,
                           City = cty.name
                       }).ToList();

            return new JsonResult(emp);
        }


        public IActionResult Create(Employee model)
        {
            if (ModelState.IsValid)
            {
                if (_context.employees.Any(e => e.Email == model.Email))
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


        [HttpPost]
        public IActionResult UploadProfileImage()
        {
            try
            {

                var file = Request.Form.Files[0];
                var webRootPath = Environment.WebRootPath;
                var imagesFolder = Path.Combine(webRootPath, "Images");

                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }

                var fileName = Path.GetFileName(file.FileName);
                var fullPath = Path.Combine(webRootPath, "Images", fileName);
                FileStream stream = new FileStream(fullPath, FileMode.Create);
                file.CopyTo(stream);
                stream.Close();
                return Json(new { Ok = true, Name = fileName });
            }
            catch (Exception ex)
            {

                return Json(new { Ok = false, Message = ex.Message });
            }

        }

        public IActionResult Update(Employee model)
        {
            if (ModelState.IsValid)
            {
                if (_context.employees.Any(e => e.Email == model.Email))
                {
                    var existingEntity = _context.employees.SingleOrDefault(p => p.Id == model.Id);
                    if (existingEntity != null)
                    {
                        _context.Entry(existingEntity).State = EntityState.Detached;
                    }
                    if (existingEntity.ProfileImg != model.ProfileImg)
                    {

                        string deletefolder = Path.Combine(Environment.WebRootPath, "Images", existingEntity.ProfileImg);
                        string currentImage = Path.Combine(Directory.GetCurrentDirectory(), deletefolder);
                        if (currentImage != null)
                        {
                            if (System.IO.File.Exists(currentImage))
                            {
                                System.IO.File.Delete(currentImage);
                            }
                        }

                    }
                    _context.employees.Update(model);
                    _context.SaveChanges();
                    return Json(new { ok = true, message = "Employee updated successfully!" });
                }
                else
                {

                    return Json(new { ok = false, message = "Employee not registered!" });
                }
            }
            else
            {

                return Json(new { ok = false, message = "something went wrong!" });
            }
        }

        public IActionResult GetEmployee(int id)
        {
            var record = _context.employees.SingleOrDefault(p => p.Id == id);
            return new JsonResult(record);
        }


        public IActionResult DeleteRecord(int id)
        {
            var data = _context.employees.SingleOrDefault(p => p.Id == id);
            if (data != null)
            {
                string deletefolder = Path.Combine(Environment.WebRootPath, "Images", data.ProfileImg);
                string currentImage = Path.Combine(Directory.GetCurrentDirectory(), deletefolder);
                if (currentImage != null)
                {
                    if (System.IO.File.Exists(currentImage))
                    {
                        System.IO.File.Delete(currentImage);
                    }
                }
                _context.employees.Remove(data);
                _context.SaveChanges();
                return Json(new { ok = true });
            }
            return RedirectToAction("Index");
        }

    }
}
