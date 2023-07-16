using Microsoft.AspNetCore.Mvc;
using PaperTrade.BusinessLogic.Services;
using PaperTrade.BusinessLogic.Services.Briefs.Requests;
using PaperTrade.Common.Models;

namespace PaperTrade.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BriefController : ControllerBase
    {
        private readonly IBriefService briefService;

        public BriefController(IBriefService briefService)
        {
            this.briefService = briefService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brief>>> Get()
        {
            var briefs = await briefService.GetAllBriefsAsync();
            return Ok(briefs);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Brief>> Get(Guid id)
        {
            var brief = await briefService.GetBriefAsync(id);
            return Ok(brief);
        }

        [HttpPost]
        public async Task<ActionResult<Brief>> Post(BriefPostRequest request)
        {
            var brief = await briefService.CreateBriefAsync(request);
            return CreatedAtAction(nameof(Get), new { id = brief.Id }, brief);
        }

        [HttpPut]
        public async Task<IActionResult> Put(BriefPutRequest request)
        {
            var brief = await briefService.UpdateBriefAsync(request);
            return Ok(brief);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await briefService.DeleteBriefAsync(id);
            return NoContent();
        }

    }
}
