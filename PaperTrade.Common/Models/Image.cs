using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PaperTrade.Common.Models
{
    public class Image
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
    }
}
