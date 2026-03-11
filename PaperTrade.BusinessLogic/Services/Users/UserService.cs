using PaperTrade.BusinessLogic.Services.Users.Requests;
using PaperTrade.Common.Models;
using PaperTrade.DataAccess.Repositories;

namespace PaperTrade.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> GetCurrentUserAsync(string objectId)
        {
            return await userRepository.GetUserFromAuthenticationAsync(objectId);
        }

        public async Task<User> CreateUserAsync(UserPostRequest request)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                ObjectIdentifier = request.ObjectIdentifier,
                Email = request.Email,
                DisplayName = request.DisplayName,
                FirstName = string.Empty,
                LastName = string.Empty,
                Briefs = new List<BasicBrief>(),
                Trades = new List<BasicTrade>()
            };

            await userRepository.CreateUserAsync(user);
            return user;
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            return await userRepository.GetUserAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await userRepository.GetAllUsersAsync();
        }
    }
}