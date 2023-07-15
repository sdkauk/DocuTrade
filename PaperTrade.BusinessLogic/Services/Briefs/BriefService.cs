using PaperTrade.BusinessLogic.Services.Briefs.Requests;
using PaperTrade.Common.Models;
using PaperTrade.DataAccess.Repositories;

namespace PaperTrade.BusinessLogic.Services
{
    public class BriefService : IBriefService
    {
        private readonly IBriefRepository briefRepository;
        private readonly IDocumentRepository documentRepository;
        private readonly IImageRepository imageRepository;
        private readonly IUserRepository userRepository;

        public BriefService(IBriefRepository briefRepository, IDocumentRepository documentRepository, IImageRepository imageRepository, IUserRepository userRepository)
        {
            this.briefRepository = briefRepository;
            this.documentRepository = documentRepository;
            this.imageRepository = imageRepository;
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<Brief>> GetAllBriefsAsync()
        {
            return await briefRepository.GetAllBriefsAsync();
        }

        public async Task<Brief> GetBriefAsync(Guid id)
        {
            return await briefRepository.GetBriefAsync(id);
        }

        public async Task<IEnumerable<Brief>> GetBriefsByAuthorAsync(Guid userId)
        {
            return await briefRepository.GetBriefsByAuthor(userId);
        }

        public async Task<Brief> CreateBriefAsync(BriefPostRequest request)
        {
            var brief = new Brief()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description
            };

            await briefRepository.CreateBriefAsync(brief);
            return brief;
        }

        public async Task<Brief> UpdateBriefAsync(BriefPutRequest request)
        {
            var brief = await briefRepository.GetBriefAsync(request.Id);

            if (brief == null)
            {
                throw new Exception($"Brief with id {request.Id} does not exist.");
            }

            if (request.Name != null)
            {
                brief.Name = request.Name;
            }
            if (request.Document.HasValue)
            {
                brief.Document = await documentRepository.GetDocumentAsync(request.Document.Value);
            }
            if (request.Preview.HasValue)
            {
                brief.Preview = await imageRepository.GetImageAsync(request.Preview.Value);
            }
            if (request.Author.HasValue)
            {
                brief.Author = new BasicUser(await userRepository.GetUserAsync(request.Author.Value));
            }
            if (request.Description != null)
            {
                brief.Description = request.Description;
            }

            await briefRepository.UpdateBriefAsync(brief);
            return brief;
        }

        public async Task DeleteBriefAsync(Guid id)
        {
            var brief = await briefRepository.GetBriefAsync(id);

            if (brief.Document != null)
            {
                await documentRepository.DeleteDocumentAsync(brief.Document.Id);
            }

            if (brief.Preview != null)
            {
                await imageRepository.DeleteImageAsync(brief.Preview.Id);
            }

            if (brief.Owners != null)
            {
                foreach (var owner in brief.Owners)
                {
                    var user = await userRepository.GetUserAsync(owner.Id);
                    user.Briefs.RemoveAll(bb => bb.Id == brief.Id);
                    await userRepository.UpdateUserAsync(user);
                }
            }
            await briefRepository.DeleteBriefAsync(id);
        }
    }
}