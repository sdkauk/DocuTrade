﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PaperTrade.Common.Models
{
    public class Trade
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public BasicUser Buyer { get; set; }
        public BasicUser Seller { get; set; }
        public BasicBrief BuyerBrief { get; set; }
        public BasicBrief SellerBrief { get; set; }
        public TradeStatus Status { get; set; }
    }
}
