using BeerAPI.Data;
using BeerAPI.Data.Interfaces;
using BeerAPI.Data.Repositories;
using BeerAPI.Models;
using BeerAPI.Repositories;
using BeerAPI.Services;
using BeerAPI.Validators;
using DotNetEnv;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
        var connectionString =
            builder.Configuration.GetConnectionString("DefaultConnection")
            ?? "Server=(LocalDb)\\MSSQLLocalDB;Database=BeerAPI;Trusted_Connection=True;Persist Security Info=False;";

        // Register the configuration class for the API Weather
        builder.Services.Configure<WeatherApiSettings>(
            builder.Configuration.GetSection("WeatherApiSettings")
        );

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddTransient<IValidator<Beer>, BeerValidator>();
        builder.Services.AddScoped<IBeerDescriptionService, BeerDescriptionService>();

        // Register DbContext
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                connectionString,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null
                    );
                }
            )
        );

        // Register Services and Repositories
        builder.Services.AddScoped<IBeerService, BeerService>();
        builder.Services.AddScoped<ITrolleyService, TrolleyService>();
        builder.Services.AddScoped<IBeerRepository, BeerRepository>();
        builder.Services.AddScoped<ITrolleyRepository, TrolleyRepository>();

        // Register the WeatherService
        builder.Services.AddHttpClient<WeatherService>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Register DatabaseSetup as a scoped service
        builder.Services.AddScoped<DatabaseSetup>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI();

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
