namespace PaperTrade.DataAccess.DataSeeder
{
    public class DataSeeder
    {
        private readonly TradeStatusSeeder tradeStatusSeeder;

        public DataSeeder(TradeStatusSeeder tradeStatusSeeder)
        {
            this.tradeStatusSeeder = tradeStatusSeeder;
        }

        public async Task SeedDefaultData()
        {
            await tradeStatusSeeder.SeedAsync();
        }

        public async Task SeedDevelopmentData()
        {
            
        }
    }
}