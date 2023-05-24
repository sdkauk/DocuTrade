using MongoDB.Driver;
using PaperTrade.Common.Models;

namespace PaperTrade.DataAccess.Repositories
{
    public class BriefRepository : IBriefRepository
    {
        private readonly IMongoCollection<Brief> briefs;
        public BriefRepository(IDbConnection db)
        {
            briefs = db.BriefCollection;
        }

        public async Task<List<Brief>> GetAllBriefsAsync()
        {
            var results = await briefs.FindAsync(_ => true);
            return results.ToList();
        }

        public async Task<Brief> GetBriefAsync(Guid id)
        {
            var results = await briefs.FindAsync(b => b.Id == id);
            return results.FirstOrDefault();
        }

        public async Task<List<Brief>> GetBriefsByAuthor(Guid userId)
        {
            var results = await briefs.FindAsync(b => b.Author.Id == userId);
            return results.ToList();
        }

        public async Task CreateBriefAsync(Brief brief)
        {
            await briefs.InsertOneAsync(brief);
        }

        public async Task UpdateBriefAsync(Brief brief)
        {
            var filter = Builders<Brief>.Filter.Eq("Id", brief.Id);
            await briefs.ReplaceOneAsync(filter, brief, new ReplaceOptions { IsUpsert = true });
        }

    }
}
