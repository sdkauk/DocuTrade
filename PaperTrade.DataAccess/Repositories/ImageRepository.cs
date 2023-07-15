using MongoDB.Driver;
using PaperTrade.Common.Models;

namespace PaperTrade.DataAccess.Repositories
{
    public class ImageRepository : IImageRepository
    {

        private readonly IMongoCollection<Image> images;
        public ImageRepository(IDbConnection db)
        {
            images = db.ImageCollection;
        }

        public async Task<List<Image>> GetAllImagesAsync()
        {
            var results = await images.FindAsync(_ => true);
            return results.ToList();
        }

        public async Task<Image> GetImageAsync(Guid id)
        {
            var results = await images.FindAsync(i => i.Id == id);
            return results.FirstOrDefault();
        }

        public async Task CreateImageAsync(Image image)
        {
            await images.InsertOneAsync(image);
        }

        public async Task UpdateImageAsync(Image image)
        {
            var filter = Builders<Image>.Filter.Eq("Id", image.Id);
            await images.ReplaceOneAsync(filter, image);
        }

        public async Task DeleteImageAsync(Guid id)
        {
            var filter = Builders<Image>.Filter.Eq("Id", id);
            await images.DeleteOneAsync(filter);
        }
    }
}
