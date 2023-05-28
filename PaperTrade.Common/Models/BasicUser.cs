using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PaperTrade.Common.Models
{
    public class BasicUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string DisplayName { get; set; }

        public BasicUser()
        {

        }

        public BasicUser(User user)
        {
            Id = user.Id;
            DisplayName = user.DisplayName;
        }
    }
}
