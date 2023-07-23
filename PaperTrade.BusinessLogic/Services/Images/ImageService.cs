using PaperTrade.DataAccess.Repositories;
using PaperTrade.DataAccess;
using PaperTrade.Common.Models;
using PaperTrade.BusinessLogic.Services.Images.Requests;

namespace PaperTrade.BusinessLogic.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository imageRepository;
        private readonly IBlobStorageService blobStorageService;

        public ImageService(IImageRepository imageRepository, IBlobStorageService blobStorageService)
        {
            this.imageRepository = imageRepository;
            this.blobStorageService = blobStorageService;
        }

        public async Task<IEnumerable<Image>> GetAllImagesAsync()
        {
            return await imageRepository.GetAllImagesAsync();
        }

        public async Task<Image> GetImageAsync(Guid id)
        {
            return await imageRepository.GetImageAsync(id);
        }

        public async Task<Stream> DownloadImageAsync(string imageName)
        {
            return await blobStorageService.DownloadBlobAsync("imagecontainer", imageName);
        }

        public async Task<Image> CreateImageAsync(ImagePostRequest request)
        {
            if (request.File == null || request.File.Length == 0)
                throw new Exception("File is null or empty");

            var extension = Path.GetExtension(request.File.FileName);
            var image = new Image { Id = Guid.NewGuid(), Extension = extension };
            var fileName = image.Id.ToString() + image.Extension;

            using (var stream = new MemoryStream())
            {
                await request.File.CopyToAsync(stream);
                stream.Position = 0;
                await blobStorageService.UploadBlobAsync("imagecontainer", fileName, stream);
            }

            await imageRepository.CreateImageAsync(image);

            return image;
        }

        public async Task DeleteImageAsync(Guid id)
        {
            var image = await imageRepository.GetImageAsync(id);

            if (image == null)
            {
                throw new Exception($"Image with id {id} does not exist.");
            }

            var fileName = image.Id.ToString() + image.Extension;
            await blobStorageService.DeleteBlobAsync("imagecontainer", fileName);
            await imageRepository.DeleteImageAsync(id);
        }
    }
}
