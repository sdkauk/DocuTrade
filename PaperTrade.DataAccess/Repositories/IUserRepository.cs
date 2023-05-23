using PaperTrade.Common.Models;

namespace PaperTrade.DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);
        Task<User> GetUserAsync(Guid id);
        Task<User> GetUserFromAuthenticationAsync(string objectId);
        Task<List<User>> GetUsersAsync();
        Task UpdateUserAsync(User user);
    }
}