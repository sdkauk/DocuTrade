using Microsoft.AspNetCore.Mvc;
using PaperTrade.BusinessLogic.Services;
using PaperTrade.BusinessLogic.Services.Images.Requests;
using PaperTrade.Common.Models;

namespace PaperTrade.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> Get()
        {
            var images = await imageService.GetAllImagesAsync();
            return Ok(images);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Image>> Get(Guid id)
        {
            var image = await imageService.GetImageAsync(id);
            return Ok(image);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Download(string fileName)
        {
            Stream imageStream = await imageService.DownloadImageAsync(fileName);

            if (imageStream == null)
            {
                return NotFound();
            }

            return File(imageStream, "image/png", fileName);
        }

        [HttpPost]
        public async Task<ActionResult<Image>> Post([FromForm] ImagePostRequest request)
        {
            var image = await imageService.CreateImageAsync(request);
            return CreatedAtAction(nameof(Get), new { id = image.Id }, image);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await imageService.DeleteImageAsync(id);
            return NoContent();
        }
    }
}
