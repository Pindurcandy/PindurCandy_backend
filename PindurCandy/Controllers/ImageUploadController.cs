using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;

namespace PindurCandy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public ImageUploadController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost]

        public IActionResult ImageUpload()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var path = _env.ContentRootPath + "/Image/" + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return Ok(fileName);
            }
            catch (Exception)
            {
                return Ok("default.jpg");
            }
        }
    }
}
