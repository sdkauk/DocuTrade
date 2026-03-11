using PaperTrade.BusinessLogic.Services.Users.Requests;
using PaperTrade.Common.Models;

namespace PaperTrade.BusinessLogic.Services
{
    public interface IUserService
    {
        Task<User> GetCurrentUserAsync(string objectId);
        Task<User> CreateUserAsync(UserPostRequest request);
        Task<User> GetUserAsync(Guid id);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}