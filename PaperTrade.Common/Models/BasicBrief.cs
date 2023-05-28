using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PaperTrade.Common.Models
{
    public class BasicBrief
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public BasicBrief()
        {

        }

        public BasicBrief(Brief brief)
        {
            Id = brief.Id;
            Name = brief.Name;
        }
    }
}
