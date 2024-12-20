﻿using PaperTrade.Common.Models;

namespace PaperTrade.DataAccess.Repositories
{
    public interface ITradeStatusRepository
    {
        Task CreateTradeStatusAsync(TradeStatus tradeStatus);
        Task DeleteTradeStatusAsync(Guid id);
        Task<List<TradeStatus>> GetAllTradeStatusesAsync();
        Task<TradeStatus> GetTradeStatusAsync(Guid id);
        Task<TradeStatus> GetTradeStatusByNameAsync(TradeStatusName name);
        Task UpdateTradeStatusAsync(TradeStatus tradeStatus);
    }
}