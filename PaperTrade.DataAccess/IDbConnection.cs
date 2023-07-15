using MongoDB.Driver;
using PaperTrade.Common.Models;

namespace PaperTrade.DataAccess
{
    public interface IDbConnection
    {
        IMongoCollection<Brief> BriefCollection { get; }
        string BriefCollectionName { get; }
        MongoClient Client { get; }
        string DbName { get; }
        IMongoCollection<Document> DocumentCollection { get; }
        string DocumentCollectionName { get; }
        IMongoCollection<Image> ImageCollection { get; }
        string ImageCollectionName { get; }
        IMongoCollection<Trade> TradeCollection { get; }
        string TradeCollectionName { get; }
        IMongoCollection<TradeStatus> TradeStatusCollection { get; }
        string TradeStatusCollectionName { get; }
        IMongoCollection<User> UserCollection { get; }
        string UserCollectionName { get; }
    }
}