using PaperTrade.BusinessLogic.Services.Trades.Requests;
using PaperTrade.Common.Models;

namespace PaperTrade.BusinessLogic.Services
{
    public interface ITradeService
    {
        Task<Trade> AcceptTradeAsync(Guid id);
        Task<Trade> CreateTradeAsync(TradePostRequest request);
        Task DeleteTradeAsync(Guid id);
        Task<IEnumerable<Trade>> GetAllTradesAsync();
        Task<Trade> GetTradeAsync(Guid id);
        Task<IEnumerable<Trade>> GetTradesByUserAsync(Guid userId);
        Task<Trade> DeclineTradeAsync(Guid id);
        Task<Trade> UpdateTradeAsync(TradePutRequest request);
    }
}