using PaperTrade.Common.Models;
using PaperTrade.DataAccess.Repositories;

public class TradeStatusSeeder
{
    private readonly ITradeStatusRepository _tradeStatusRepository;

    public TradeStatusSeeder(ITradeStatusRepository tradeStatusRepository)
    {
        _tradeStatusRepository = tradeStatusRepository;
    }

    public async Task SeedAsync()
    {
        var tradeStatuses = new List<TradeStatus>
        {
            new TradeStatus { Id = Guid.NewGuid(), Name = "Accepted", Description = "Trade has been accepted." },
            new TradeStatus { Id = Guid.NewGuid(), Name = "Declined", Description = "Trade has been declined." },
            new TradeStatus { Id = Guid.NewGuid(), Name = "Pending", Description = "Trade is pending." },
        };

        var existingTradeStatuses = await _tradeStatusRepository.GetAllTradeStatusesAsync();

        foreach (var status in tradeStatuses)
        {
            // Check if the status already exists based on Name as the Id is generated anew each time
            if (!existingTradeStatuses.Any(s => s.Name == status.Name))
            {
                await _tradeStatusRepository.CreateTradeStatusAsync(status);
            }
        }
    }
}