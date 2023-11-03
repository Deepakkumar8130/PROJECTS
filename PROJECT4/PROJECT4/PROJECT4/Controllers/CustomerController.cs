using Microsoft.AspNetCore.Mvc;
using PROJECT4.Data;
using PROJECT4.Models;

namespace PROJECT4.Controllers
{
    public class CustomerController : Controller
    {
        CustomerService cs = new CustomerService();
        public Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment { get; }

        public CustomerController(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            Environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Index(Customer customer)
        //{
        //    return View();
        //}

        public IActionResult GetCountry()
        {
            return new JsonResult(cs.GetCountry());
        } 
        
        public IActionResult GetState(int id)
        {
            return new JsonResult(cs.GetState(id));
        } 
        
        public IActionResult GetCity(int id)
        {
            return new JsonResult(cs.GetCity(id));
        } 
        
        public IActionResult GetCustomers()
        {
            return new JsonResult(cs.GetCustomers());
        }
        
        public IActionResult GetBinCustomers()
        {
            return new JsonResult(cs.GetBinCustomers());
        }
        
        public IActionResult GetCustomer(int id)
        {
            return new JsonResult(cs.GetCustomer(id));
        }
        
        
        public IActionResult CreateRecord(Customer customer)
        {
            if(ModelState.IsValid)
            {
                var result = cs.CreateRecord(customer);
                if (result == Models.Action.Success)
                {
                    return new JsonResult(new { ok = true, message = " Customer Record Created Successfully" });
                }
                else if (result == Models.Action.EmailExist)
                {
                    return new JsonResult(new { ok = false, message = " Customer E-mail Already Successfully" });
                }
                return new JsonResult(new { ok = false, message = "Something Went Error" });
            }
            return new JsonResult(new { ok = false, message = "Something Went Error" });
        }
        
        public IActionResult UpdateRecord(Customer customer)
        {
            if(ModelState.IsValid)
            {
                var result = cs.UpdateRecord(customer);
                if (result == Models.Action.Success)
                {
                    return new JsonResult(new { ok = true, message = " Customer Record Update Successfully" });
                }
                else if (result == Models.Action.NotRegistered)
                {
                    return new JsonResult(new { ok = false, message = " Customer  Not Registered" });
                }
                return new JsonResult(new { ok = false, message = "Something Went Error" });
            }
            return new JsonResult(new { ok = false, message = "Something Went Error" });
        }
        
        
        public IActionResult DeleteRecord(int id)
        {
            var result = cs.DeleteRecord(id);
            if(result== Models.Action.Success)
            {
                return new JsonResult( new {ok=true, message=" Customer Record Delete Successfully"});
            }
            else if(result== Models.Action.NotRegistered)
            {
                return new JsonResult(new { ok = false, message = " Customer  Not Registered" });
            }
            return new JsonResult(new { ok = false, message = "Something Went Error" });
        }
        
        public IActionResult PermanentDeleteRecord(int id)
        {
            var result = cs.PermanentDeleteRecord(id);
            if(result== Models.Action.Success)
            {
                return new JsonResult( new {ok=true, message=" Customer Record Delete Successfully"});
            }
            else if(result== Models.Action.NotRegistered)
            {
                return new JsonResult(new { ok = false, message = " Customer  Not Registered" });
            }
            return new JsonResult(new { ok = false, message = "Something Went Error" });
        }
        
        public IActionResult UndoRecord(int id)
        {
            var result = cs.UndoRecord(id);
            if(result== Models.Action.Success)
            {
                return new JsonResult( new {ok=true, message=" Customer Record Recover Successfully"});
            }
            else if(result== Models.Action.NotFound)
            {
                return new JsonResult(new { ok = false, message = " Customer  Not Found" });
            }
            return new JsonResult(new { ok = false, message = "Something Went Error" });
        }


        public IActionResult Bin()
        {
            return View();
        }


        public IActionResult UpdateProfileImage()
        {
            var id = Request.Form["id"];
            var file = Request.Form.Files[0];
            var rootPath = Environment.WebRootPath;
            var fullPath = Path.Combine(rootPath, "Images", file.FileName);

            FileStream stream = new FileStream(fullPath, FileMode.Create);
            file.CopyTo(stream);

            cs.UpdateProfileImage(Convert.ToInt32(id),"Images/" + file.FileName);

            return new JsonResult(new { ok = true, message = " Image Upload Successfully" });
        }
    }
}
