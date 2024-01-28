using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiseProject.Data;
using PractiseProject.Models;

namespace PractiseProject.Controllers
{
    public class DataController : Controller
    {
		private ApplicationDbContext DbContext { get; }

		public DataController(ApplicationDbContext dbContext)
		{
			DbContext = dbContext;
		}

		public IActionResult Index()
        {

			List<model1> Data = DbContext.Records.ToList();
            return View(Data);
        }

		public IActionResult CreateOrEdit(int Id = 0)
		{
			if (Id == 0)
			{
				return View(new model1() { Id = 0});
			}
			else
			{
				model1? model = DbContext.Records.FirstOrDefault(e => e.Id == Id);
				if (model == null)
				{
					return NotFound();
				}
				return View(model);
			}
		}


		[HttpPost]
		public IActionResult CreateOrEdit(model1 model)
		{
			if (ModelState.IsValid)
			{
				if (model.Id == 0)
				{
					DbContext.Records.Add(model);
					DbContext.SaveChanges();
					return RedirectToAction("Index");
				}
				DbContext.Records.Update(model);
				DbContext.SaveChanges();
				return RedirectToAction("Index");
			}
			else
			{
				return View(model);
			}
		}

		public IActionResult Delete(int? id)
		{
			model1? model = DbContext.Records.FirstOrDefault(e => e.Id == id);
			if (model == null)
			{
				return NotFound();
			}
			DbContext.Records.Remove(model);
			DbContext.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
