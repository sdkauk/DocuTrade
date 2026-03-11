using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using PaperTrade.API;
using PaperTrade.API.Extensions;
using PaperTrade.DataAccess.DataSeeder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.ConfigureServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        var origins = builder.Configuration.GetSection("CorsOrigins").Get<string[]>() ?? Array.Empty<string>();
        policy.WithOrigins(origins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));


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
    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    seeder.SeedDevelopmentData().Wait();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();
app.UseUserSynchronization();

app.MapControllers();

app.Run();
