using PaperTrade.Common.Models;

namespace PaperTrade.DataAccess.Repositories
{
    public interface ITradeRepository
    {
        Task CreateTradeAsync(Trade trade);
        Task DeleteTradeAsync(Guid id);
        Task<List<Trade>> GetAllTradesAsync();
        Task<Trade> GetTradeAsync(Guid id);
        Task<List<Trade>> GetTradesByUserAsync(Guid userId);
        Task UpdateTradeAsync(Trade trade);
    }
}