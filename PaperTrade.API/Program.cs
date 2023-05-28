using PaperTrade.API;
using PaperTrade.DataAccess.DataSeeder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.ConfigureServices();

var app = builder.Build();

// Seed default data always
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    seeder.SeedDefaultData().Wait();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Seed additional development data
    using (var scope = app.Services.CreateScope())
    {
        var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
        seeder.SeedDevelopmentData().Wait();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
