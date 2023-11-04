using MAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClaimRasieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        public Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment { get; set; }
        public ClaimController(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            Environment = environment;
        }

        [HttpPost]
        [Route("ClaimRequest")]
        public async Task<IActionResult> ClaimRequest()
        {
            var formData = Request.Form["claim"];
            var formEvidence = Request.Form.Files[0];

            var claim = JsonConvert.DeserializeObject<ClaimRequestModel>(formData);
            claim.Evidence = UploadEvidence(formEvidence);

            return Ok("successfully");
        }

        private string UploadEvidence(IFormFile file)
        {
            var roothPath = Environment.ContentRootPath;
            var ext = Path.GetExtension(file.FileName);
            var folderPath = Path.Combine(roothPath, "EvidenceFiles");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string fileName = "Evidence"+Guid.NewGuid().ToString()+ext;

            var fullPath = Path.Combine(roothPath, "EvidenceFiles", fileName);

            FileStream stream = new FileStream(fullPath, FileMode.Create);
            file.CopyTo(stream);
            return "EvidenceFiles/" + fileName;
        }
    }
}
