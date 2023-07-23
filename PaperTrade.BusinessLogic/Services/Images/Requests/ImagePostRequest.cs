
using Microsoft.AspNetCore.Http;

namespace PaperTrade.BusinessLogic.Services.Images.Requests
{
    public class ImagePostRequest
    {
        public IFormFile File { get; set; }
    }
}
