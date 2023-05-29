namespace PaperTrade.DataAccess.DataSeeder
{
    public class DataSeeder
    {
        private readonly TradeStatusSeeder tradeStatusSeeder;
        private readonly ImageSeeder imageSeeder;
        private readonly DocumentSeeder documentSeeder;
        private readonly UserSeeder userSeeder;
        private readonly BriefSeeder briefSeeder;

        public DataSeeder(TradeStatusSeeder tradeStatusSeeder, ImageSeeder imageSeeder, DocumentSeeder documentSeeder,
                            UserSeeder userSeeder, BriefSeeder briefSeeder)
        {
            this.tradeStatusSeeder = tradeStatusSeeder;
            this.imageSeeder = imageSeeder;
            this.documentSeeder = documentSeeder;
            this.userSeeder = userSeeder;
            this.briefSeeder = briefSeeder;
        }

        public async Task SeedDefaultData()
        {
            await tradeStatusSeeder.SeedAsync();
        }

        public async Task SeedDevelopmentData()
        {
            await imageSeeder.SeedAsync();
            await documentSeeder.SeedAsync();
            await userSeeder.SeedAsync();
            await briefSeeder.SeedAsync();
        }
    }
}