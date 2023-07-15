using PaperTrade.BusinessLogic.Services.Briefs.Requests;
using PaperTrade.Common.Models;

namespace PaperTrade.BusinessLogic.Services
{
    public interface IBriefService
    {
        Task<Brief> CreateBriefAsync(BriefPostRequest request);
        Task DeleteBriefAsync(Guid id);
        Task<IEnumerable<Brief>> GetAllBriefsAsync();
        Task<Brief> GetBriefAsync(Guid id);
        Task<IEnumerable<Brief>> GetBriefsByAuthorAsync(Guid userId);
        Task<Brief> UpdateBriefAsync(BriefPutRequest request);
    }
}