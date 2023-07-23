using PaperTrade.BusinessLogic.Services.Images.Requests;
using PaperTrade.Common.Models;

namespace PaperTrade.BusinessLogic.Services
{
    public interface IImageService
    {
        Task<Image> CreateImageAsync(ImagePostRequest request);
        Task DeleteImageAsync(Guid id);
        Task<Stream> DownloadImageAsync(string imageName);
        Task<IEnumerable<Image>> GetAllImagesAsync();
        Task<Image> GetImageAsync(Guid id);
    }
}