using PaperTrade.BusinessLogic.Services;
using PaperTrade.DataAccess;
using PaperTrade.DataAccess.DataSeeder;
using PaperTrade.DataAccess.Repositories;

namespace PaperTrade.API
{
    public static class RegisterServices
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IDbConnection, DbConnection>();

            builder.Services.AddSingleton<IBlobStorageService>(new BlobStorageService(builder.Configuration.GetConnectionString("BlobStorage")));

            builder.Services.AddScoped<ITradeStatusRepository, TradeStatusRepository>();
            builder.Services.AddScoped<IImageRepository, ImageRepository>();
            builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IBriefRepository, BriefRepository>();
            builder.Services.AddScoped<ITradeRepository, TradeRepository>();

            builder.Services.AddTransient<TradeStatusSeeder>();
            builder.Services.AddTransient<ImageSeeder>();
            builder.Services.AddTransient<DocumentSeeder>();
            builder.Services.AddTransient<UserSeeder>();
            builder.Services.AddTransient<BriefSeeder>();

            builder.Services.AddTransient<DataSeeder>();

            builder.Services.AddScoped<IBriefService, BriefService>();
            builder.Services.AddScoped<IDocumentService, DocumentService>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<ITradeService, TradeService>();

            builder.Services.AddControllers();
        }
    }
}
