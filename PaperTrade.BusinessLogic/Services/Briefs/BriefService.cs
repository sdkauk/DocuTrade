using PaperTrade.BusinessLogic.Services.Briefs.Requests;
using PaperTrade.Common.Models;
using PaperTrade.DataAccess.Repositories;

namespace PaperTrade.API.Services
{
    public class BriefService
    {
        private readonly IBriefRepository briefRepository;

        public BriefService(IBriefRepository briefRepository)
        {
            this.briefRepository = briefRepository;
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

        public async Task<Brief> CreateBriefAsync(PostRequest request)
        {
            brief.Id = Guid.NewGuid();
            await briefRepository.CreateBriefAsync(brief);
            return brief;
        }

        public async Task UpdateBriefAsync(Brief brief)
        {
            await briefRepository.UpdateBriefAsync(brief);
        }

        public async Task DeleteBriefAsync(Guid id)
        {
            await briefRepository.DeleteBriefAsync(id);
        }
    }
}