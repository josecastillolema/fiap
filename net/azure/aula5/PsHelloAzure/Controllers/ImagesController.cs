using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PsHelloAzure.Models;
using PsHelloAzure.Services;

namespace PsHelloAzure.Controllers
{
    [Route("[controller]/[action]")]
    public class ImagesController : Controller
    {
        private readonly ImageStore imageStore;

        public ImagesController(ImageStore imageStore)
        {
            this.imageStore = imageStore;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile image)
        {
            if(image != null)
            {
                using (var stream = image.OpenReadStream())
                { 
                    var imageId = await imageStore.SaveImage(stream);
                    return RedirectToAction("Show", new { imageId });
                }
            }
            return View();
        }

        [HttpGet("{imageId}")]
        public ActionResult Show(string imageId)
        {
            var model = new ShowModel { Uri = imageStore.UriFor(imageId) };
            return View(model);
        }
    }
}