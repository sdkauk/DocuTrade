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
            builder.Services.AddTransient<TradeStatusSeeder>();
            builder.Services.AddTransient<DataSeeder>();
        }
    }
}
