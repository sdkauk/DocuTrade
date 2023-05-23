using MongoDB.Driver;
using PaperTrade.Common.Models;

namespace PaperTrade.DataAccess.Repositories
{
    public class BriefRepository
    {
        private readonly IMongoCollection<Brief> briefs;
        public BriefRepository(IDbConnection db)
        {
            briefs = db.BriefCollection;
        }

        public async Task<List<Brief>> GetBriefsAsync()
        {
            var results = await briefs.FindAsync(_ => true);
            return results.ToList();
        }

        public async Task<Brief> GetBriefAsync(Guid id)
        {
            var results = await briefs.FindAsync(b => b.Id == id);
            return results.FirstOrDefault();
        }

        public async Task<List<Brief>> GetBriefsByUser(Guid userId)
        {
            var results = await briefs.FindAsync(b => b.Author.Id == userId);
            return results.ToList();
        }


    }
}
