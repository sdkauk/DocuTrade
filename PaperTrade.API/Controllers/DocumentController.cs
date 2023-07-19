using Microsoft.AspNetCore.Mvc;
using PaperTrade.BusinessLogic.Services;
using PaperTrade.BusinessLogic.Services.Documents.Requests;
using PaperTrade.Common.Models;
using System.IO;

namespace PaperTrade.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService documentService;

        public DocumentController(IDocumentService documentService)
        {
            this.documentService = documentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> Get()
        {
            var documents = await documentService.GetAllDocumentsAsync();
            return Ok(documents);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Document>> Get(Guid id)
        {
            var document = await documentService.GetDocumentAsync(id);
            return Ok(document);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Download(string fileName)
        {
            Stream documentStream = await documentService.DownloadDocumentAsync(fileName);

            if (documentStream == null)
            {
                return NotFound();
            }

            return File(documentStream, "application/octet-stream", fileName);
        }

        [HttpPost]
        public async Task<ActionResult<Document>> Post([FromForm] DocumentPostRequest request)
        {
            var document = await documentService.CreateDocumentAsync(request);
            return CreatedAtAction(nameof(Get), new { id = document.Id }, document);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await documentService.DeleteDocumentAsync(id);
            return NoContent();
        }
    }
}
