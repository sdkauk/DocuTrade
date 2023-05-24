using PaperTrade.Common.Models;

namespace PaperTrade.DataAccess.Repositories
{
    public interface IImageRepository
    {
        Task CreateImageAsync(Image image);
        Task DeleteImageAsync(Guid id);
        Task<List<Image>> GetAllImagesAsync();
        Task<Image> GetImageByIdAsync(Guid id);
        Task UpdateImageAsync(Image image);
    }
}