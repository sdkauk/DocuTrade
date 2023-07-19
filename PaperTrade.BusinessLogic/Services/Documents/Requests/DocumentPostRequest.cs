using Microsoft.AspNetCore.Http;

namespace PaperTrade.BusinessLogic.Services.Documents.Requests
{
    public class DocumentPostRequest
    {
        public IFormFile File { get; set; }

    }
}
