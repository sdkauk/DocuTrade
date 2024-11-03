namespace PaperTrade.BusinessLogic.Services.Trades.Requests
{
    public class TradePostRequest
    {
        public string Name { get; set; }
        public Guid BuyerId { get; set; }
        public Guid SellerId { get; set; }
        public Guid BuyerBriefId { get; set; }
        public Guid SellerBriefId { get; set; }
    }
}