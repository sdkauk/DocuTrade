using PaperTrade.BusinessLogic.Services.Trades.Requests;
using PaperTrade.Common.Models;
using PaperTrade.DataAccess.Repositories;

namespace PaperTrade.BusinessLogic.Services
{
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository tradeRepository;
        private readonly IUserRepository userRepository;
        private readonly IBriefRepository briefRepository;
        private readonly ITradeStatusRepository tradeStatusRepository;

        public TradeService(ITradeRepository tradeRepository, IUserRepository userRepository,
            IBriefRepository briefRepository, ITradeStatusRepository tradeStatusRepository)
        {
            this.tradeRepository = tradeRepository;
            this.userRepository = userRepository;
            this.briefRepository = briefRepository;
            this.tradeStatusRepository = tradeStatusRepository;
        }

        public async Task<IEnumerable<Trade>> GetAllTradesAsync()
        {
            return await tradeRepository.GetAllTradesAsync();
        }

        public async Task<Trade> GetTradeAsync(Guid id)
        {
            return await tradeRepository.GetTradeAsync(id);
        }

        public async Task<IEnumerable<Trade>> GetTradesByUserAsync(Guid userId)
        {
            return await tradeRepository.GetTradesByUserAsync(userId);
        }

        public async Task<Trade> CreateTradeAsync(TradePostRequest request)
        {

            var buyerBrief = await briefRepository.GetBriefAsync(request.BuyerBriefId);
            if (buyerBrief == null)
            {
                throw new Exception($"Buyer's brief with ID {request.BuyerBriefId} not found.");
            }
            if (buyerBrief.Owners.Any(o => o.Id == request.SellerId))
            {
                throw new Exception("The seller is already an owner of the buyer's brief.");
            }

            var sellerBrief = await briefRepository.GetBriefAsync(request.SellerBriefId);
            if (sellerBrief == null)
            {
                throw new Exception($"Seller's brief with ID {request.SellerBriefId} not found.");
            }
            if (sellerBrief.Owners.Any(o => o.Id == request.BuyerId))
            {
                throw new Exception("The buyer is already an owner of the seller's brief.");
            }

            var trade = new Trade
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Buyer = new BasicUser(await userRepository.GetUserAsync(request.BuyerId)),
                Seller = new BasicUser(await userRepository.GetUserAsync(request.SellerId)),
                BuyerBrief = new BasicBrief(buyerBrief),
                SellerBrief = new BasicBrief(sellerBrief),
                Status = await tradeStatusRepository.GetTradeStatusByNameAsync(TradeStatusName.Pending)
        };

            await tradeRepository.CreateTradeAsync(trade);
            return trade;
        }

        public async Task<Trade> UpdateTradeAsync(TradePutRequest request)
        {
            var trade = await tradeRepository.GetTradeAsync(request.Id);

            if (trade == null)
            {
                throw new Exception($"Trade with id {request.Id} does not exist.");
            }

            if (request.Name != null)
            {
                trade.Name = request.Name;
            }
            if (request.BuyerId.HasValue)
            {
                trade.Buyer = new BasicUser(await userRepository.GetUserAsync(request.BuyerId.Value));
            }
            if (request.SellerId.HasValue)
            {
                trade.Seller = new BasicUser(await userRepository.GetUserAsync(request.SellerId.Value));
            }
            if (request.BuyerBriefId.HasValue)
            {
                trade.BuyerBrief = new BasicBrief(await briefRepository.GetBriefAsync(request.BuyerBriefId.Value));
            }
            if (request.SellerBriefId.HasValue)
            {
                trade.SellerBrief = new BasicBrief(await briefRepository.GetBriefAsync(request.SellerBriefId.Value));
            }

            await tradeRepository.UpdateTradeAsync(trade);
            return trade;
        }

        public async Task<Trade> AcceptTradeAsync(Guid id)
        {
            var trade = await tradeRepository.GetTradeAsync(id);
            if (trade == null)
            {
                throw new Exception($"Trade with ID {id} was not found");
            }

            var buyerBrief = await briefRepository.GetBriefAsync(trade.BuyerBrief.Id);
            if (buyerBrief != null)
            {
                buyerBrief.Owners.Add(trade.Seller);
                await briefRepository.UpdateBriefAsync(buyerBrief);
            }

            var sellerBrief = await briefRepository.GetBriefAsync(trade.SellerBrief.Id);
            if (sellerBrief != null)
            {
                sellerBrief.Owners.Add(trade.Buyer);
                await briefRepository.UpdateBriefAsync(sellerBrief);
            }

            trade.Status = await tradeStatusRepository.GetTradeStatusByNameAsync(TradeStatusName.Accepted);
            await tradeRepository.UpdateTradeAsync(trade);

            return trade;
        }

        public async Task<Trade> DeclineTradeAsync(Guid id)
        {
            var trade = await tradeRepository.GetTradeAsync(id);
            if (trade == null)
            {
                throw new Exception($"Trade with ID {id} was not found");
            }

            trade.Status = await tradeStatusRepository.GetTradeStatusByNameAsync(TradeStatusName.Declined);
            await tradeRepository.UpdateTradeAsync(trade);

            return trade;
        }

        public async Task DeleteTradeAsync(Guid id)
        {
            await tradeRepository.DeleteTradeAsync(id);
        }
    }
}

