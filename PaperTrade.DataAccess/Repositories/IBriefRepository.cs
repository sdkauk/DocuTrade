using PaperTrade.Common.Models;

namespace PaperTrade.DataAccess.Repositories
{
    public interface IBriefRepository
    {
        Task CreateBriefAsync(Brief brief);
        Task<List<Brief>> GetAllBriefsAsync();
        Task<Brief> GetBriefAsync(Guid id);
        Task<List<Brief>> GetBriefsByAuthor(Guid userId);
        Task UpdateBriefAsync(Brief brief);
    }
}