
using Microsoft.AspNetCore.Mvc;
using PROJECT5.Data;
using OfficeOpenXml;
using PROJECT5.Models;

namespace PROJECT5.Controllers
{
	public class ExcelController : Controller
	{
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ExcelController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {

            if (file != null && file.Length > 0)
            {
                // Ensure the uploads folder exists
                var uploadsFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolderPath);

                var filePath = Path.Combine(uploadsFolderPath, Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++) // Start from row 2 to skip headers
                    {
                        var student = new Student()
                        {
                            Name = worksheet.Cells[row, 1].Value.ToString(),
                            Email = worksheet.Cells[row, 2].Value.ToString(),
                            Phone = worksheet.Cells[row, 3].Value.ToString(),
                            Gender = worksheet.Cells[row, 4].Value.ToString(),
                            Course = worksheet.Cells[row, 5].Value.ToString()
                        };

                        _db.Students.Add(student);
                    }

                    _db.SaveChanges();
                }
            }

            return RedirectToAction("Index", "Student"); // Redirect to a page showing the imported data
        }
    }
}
