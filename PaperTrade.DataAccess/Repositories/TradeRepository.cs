using MongoDB.Driver;
using PaperTrade.Common.Models;

namespace PaperTrade.DataAccess.Repositories
{
    public class TradeRepository : ITradeRepository
    {

        private readonly IMongoCollection<Trade> trades;

        public TradeRepository(IDbConnection db)
        {
            trades = db.TradeCollection;
        }

        public async Task<List<Trade>> GetAllTradesAsync()
        {
            var results = await trades.FindAsync(_ => true);
            return results.ToList();
        }

        public async Task<Trade> GetTradeAsync(Guid id)
        {
            var results = await trades.FindAsync(t => t.Id == id);
            return results.FirstOrDefault();
        }

        public async Task CreateTradeAsync(Trade trade)
        {
            await trades.InsertOneAsync(trade);
        }

        public async Task UpdateTradeAsync(Trade trade)
        {
            var filter = Builders<Trade>.Filter.Eq("Id", trade.Id);
            await trades.ReplaceOneAsync(filter, trade);
        }

        public async Task DeleteTradeAsync(Guid id)
        {
            var filter = Builders<Trade>.Filter.Eq("Id", id);
            await trades.DeleteOneAsync(filter);
        }
    }
}
}
