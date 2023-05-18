using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PaperTrade.Common.Models
{
    public class Trade
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public BasicUser BuyerId { get; set; }
        public BasicUser SellerId { get; set; }
        public BasicBrief BuyerBrief { get; set; }
        public BasicBrief SellerBrief { get; set; }
        public TradeStatus Status { get; set; }
    }
}
