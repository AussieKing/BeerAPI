using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using DotNetEnv;
using FluentValidation;
using FluentValidation.AspNetCore;
using BeerAPI.Validators;
using BeerAPI.Models;
using BeerAPI.Services;
using BeerAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using BeerAPI.Data;
using BeerAPI.Data.Interfaces;
using BeerAPI.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.ApplicationInsights.Extensibility;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Loading .env variables
        DotNetEnv.Env.Load();

        // getting Application Insights Instrumentation Key and Connection String from .env
        var appInsightsKey = Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY");

        // Add Application Insights
        builder.Services.AddApplicationInsightsTelemetry(appInsightsKey);

        // Connection string configuration for SQL Server
        var connectionString = $"Server=tcp:{Environment.GetEnvironmentVariable("DB_SERVER")},1433;Initial Catalog={Environment.GetEnvironmentVariable("DB_DATABASE")};Persist Security Info=False;User ID={Environment.GetEnvironmentVariable("DB_USER")};Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddTransient<IValidator<Beer>, BeerValidator>();
        builder.Services.AddScoped<IBeerDescriptionService, BeerDescriptionService>();


        // Register DbContext
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Register Services and Repositories
        builder.Services.AddScoped<IBeerService, BeerService>();
        builder.Services.AddScoped<ITrolleyService, TrolleyService>();
        builder.Services.AddScoped<IBeerRepository, BeerRepository>();
        builder.Services.AddScoped<ITrolleyRepository, TrolleyRepository>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Register DatabaseSetup as a scoped service
        builder.Services.AddScoped<DatabaseSetup>();

        // Adding Logging to debug
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        /*if (app.Environment.IsDevelopment())
        {*/
        app.UseSwagger();
        app.UseSwaggerUI();
        /*}*/

        app.UseStaticFiles();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        // Ensure database is setup within a scope
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
            var databaseSetup = scope.ServiceProvider.GetRequiredService<DatabaseSetup>();
            databaseSetup.InitializeDatabase();
        }

        // Simple default Route
        app.MapGet("/", () => "Welcome to the Beer API! 🍺 🍻 ");

        app.Run();
    }
}
