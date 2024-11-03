using MongoDB.Driver;
using PaperTrade.Common.Models;

namespace PaperTrade.DataAccess.Repositories
{
    public class TradeStatusRepository : ITradeStatusRepository
    {

        private readonly IMongoCollection<TradeStatus> tradeStatuses;

        public TradeStatusRepository(IDbConnection db)
        {
            tradeStatuses = db.TradeStatusCollection;
        }

        public async Task<List<TradeStatus>> GetAllTradeStatusesAsync()
        {
            var results = await tradeStatuses.FindAsync(_ => true);
            return results.ToList();
        }

        public async Task<TradeStatus> GetTradeStatusAsync(Guid id)
        {
            var results = await tradeStatuses.FindAsync(ts => ts.Id == id);
            return results.FirstOrDefault();
        }

        public async Task<TradeStatus> GetTradeStatusByNameAsync(TradeStatusName name)
        {
            var filter = Builders<TradeStatus>.Filter.Eq(t => t.Name, name);
            return await tradeStatuses.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateTradeStatusAsync(TradeStatus tradeStatus)
        {
            await tradeStatuses.InsertOneAsync(tradeStatus);
        }

        public async Task UpdateTradeStatusAsync(TradeStatus tradeStatus)
        {
            var filter = Builders<TradeStatus>.Filter.Eq("Id", tradeStatus.Id);
            await tradeStatuses.ReplaceOneAsync(filter, tradeStatus);
        }

        public async Task DeleteTradeStatusAsync(Guid id)
        {
            var filter = Builders<TradeStatus>.Filter.Eq("Id", id);
            await tradeStatuses.DeleteOneAsync(filter);
        }
    }
}
