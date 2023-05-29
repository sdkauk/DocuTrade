using PaperTrade.Common.Models;
using PaperTrade.DataAccess.Repositories;

public class UserSeeder
{
    private readonly IUserRepository userRepository;

    public UserSeeder(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task SeedAsync()
    {
        var users = new List<User>
        {
            new User
            {
                Id = Guid.NewGuid(),
                ObjectIdentifier = "User1",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                DisplayName = "John Doe",
                Briefs = new List<BasicBrief>(),
                Trades = new List<BasicTrade>()
            },
            new User
            {
                Id = Guid.NewGuid(),
                ObjectIdentifier = "User2",
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@example.com",
                DisplayName = "Jane Doe",
                Briefs = new List<BasicBrief>(),
                Trades = new List<BasicTrade>()
            }
        };

        var existingUsers = await userRepository.GetAllUsersAsync();

        foreach (var user in users)
        {
            if (!existingUsers.Any())
            {
                await userRepository.CreateUserAsync(user);
            }
        }
    }
}