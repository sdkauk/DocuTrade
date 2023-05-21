using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using PaperTrade.Common.Models;

namespace PaperTrade.DataAccess
{
    public class DbConnection : IDbConnection
    {
        private readonly IConfiguration configuration;
        private readonly IMongoDatabase db;
        private string connectionId = "MongoDB";
        public string DbName { get; private set; }
        public string BriefCollectionName { get; private set; } = "briefs";
        public string UserCollectionName { get; private set; } = "users";
        public string TradeCollectionName { get; private set; } = "trades";
        public string TradeStatusCollectionName { get; private set; } = "tradestatuses";
        public string DocumentCollectionName { get; private set; } = "documents";
        public string ImageCollectionName { get; private set; } = "images";

        public MongoClient Client { get; private set; }
        public IMongoCollection<Brief> BriefCollection { get; private set; }
        public IMongoCollection<User> UserCollection { get; private set; }
        public IMongoCollection<Trade> TradeCollection { get; private set; }
        public IMongoCollection<TradeStatus> TradeStatusCollection { get; private set; }
        public IMongoCollection<Document> DocumentCollection { get; private set; }
        public IMongoCollection<Image> ImageCollection { get; private set; }

        public DbConnection(IConfiguration configuration)
        {
            this.configuration = configuration;
            Client = new MongoClient(configuration.GetConnectionString(connectionId));
            DbName = configuration["DatabaseName"];
            db = Client.GetDatabase(DbName);

            BriefCollection = db.GetCollection<Brief>(BriefCollectionName);
            UserCollection = db.GetCollection<User>(UserCollectionName);
            TradeCollection = db.GetCollection<Trade>(TradeCollectionName);
            TradeStatusCollection = db.GetCollection<TradeStatus>(TradeStatusCollectionName);
            DocumentCollection = db.GetCollection<Document>(DocumentCollectionName);
            ImageCollection = db.GetCollection<Image>(ImageCollectionName);
        }

    }
}
