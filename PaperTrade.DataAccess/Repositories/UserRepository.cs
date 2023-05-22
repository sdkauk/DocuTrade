using MongoDB.Driver;
using PaperTrade.Common.Models;

namespace PaperTrade.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> users;
        public UserRepository(IDbConnection db)
        {
            users = db.UserCollection;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var results = await users.FindAsync(_ => true);
            return results.ToList();
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            var results = await users.FindAsync(u => u.Id == id);
            return results.FirstOrDefault();
        }

        public async Task<User> GetUserFromAuthenticationAsync(string objectId)
        {
            var results = await users.FindAsync(u => u.ObjectIdentifier == objectId);
            return results.FirstOrDefault();
        }

        public Task CreateUser(User user)
        {
            return users.InsertOneAsync(user);
        }

        public Task UpdateUser(User user)
        {
            var filter = Builders<User>.Filter.Eq("Id", user.Id);
            return users.ReplaceOneAsync(filter, user, new ReplaceOptions { IsUpsert = true });
        }

    }
}
