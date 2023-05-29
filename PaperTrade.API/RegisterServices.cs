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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IDbConnection, DbConnection>();

            builder.Services.AddScoped<ITradeStatusRepository, TradeStatusRepository>();
            builder.Services.AddScoped<IImageRepository, ImageRepository>();
            builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IBriefRepository, BriefRepository>();

            builder.Services.AddTransient<TradeStatusSeeder>();
            builder.Services.AddTransient<ImageSeeder>();
            builder.Services.AddTransient<DocumentSeeder>();
            builder.Services.AddTransient<UserSeeder>();
            builder.Services.AddTransient<BriefSeeder>();

            builder.Services.AddTransient<DataSeeder>();
        }
    }
}
