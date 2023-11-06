using BAL.Interfaces;
using ClaimAPI.Models;
using MAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClaimRasieAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment { get; set; }
        private IClaimService _claimService { get; set; }
        private APIResponse response = new APIResponse();

        public ClaimController(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, IClaimService claimService)
        {
            Environment = environment;
            _claimService = claimService;
        }

        [HttpPost]
        [Route("ClaimRequest")]
        public async Task<IActionResult> ClaimRequest()
        {
            try
            {
                var formData = Request.Form["claim"];
                var formEvidence = Request.Form.Files[0];

                var claim = JsonConvert.DeserializeObject<ClaimRequestModel>(formData);
                claim.Evidence = UploadEvidence(formEvidence);
                var result = await _claimService.RaiseClaimRequest(claim);

                if (string.IsNullOrEmpty(result))
                {
                    response.status = 200;
                    response.ok = true;
                    response.data = null;
                    response.message = "Claim request rasied successfully!";
                }
                else
                {
                    response.status = 505;
                    response.ok = false;
                    response.data = null;
                    response.message = result;
                }
            }
            catch (Exception ex)
            {
                response.status = 505;
                response.ok = false;
                response.data = null;
                response.message = ex.Message;
            }
            return Ok(response);
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
