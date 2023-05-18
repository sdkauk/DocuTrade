using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PaperTrade.Common.Models
{
    public class BasicTrade
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public BasicTrade()
        {

        }

        public BasicTrade(TradeStatus trade)
        {
            Id = trade.Id;
            Name = trade.Name;
        }
    }
}
