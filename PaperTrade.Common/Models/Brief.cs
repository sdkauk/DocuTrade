using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PaperTrade.Common.Models
{
    public class Brief
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Document Document { get; set; }
        public Image Preview { get; set; }
        public BasicUser Author { get; set; }
        public List<Guid> Owners { get; set; }
        public string Description { get; set; }
    }
}
