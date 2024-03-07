using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using BeerAPI.Validators;
using BeerAPI.Models;
using BeerAPI.Services; //! Adding this to use the BeerDescriptionService

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers(); //! Adding this line to add support for MVC controllers
builder.Services.AddFluentValidationAutoValidation(); //!  Register FluentValidation's automatic validation. This integrates FluentValidation into the ASP.NET Core pipeline.
builder.Services.AddTransient<IValidator<Beer>, BeerValidator>(); //! Register the BeerValidator for dependency injection, specifying that whenever IValidator<Beer> is needed, a BeerValidator should be provided.
builder.Services.AddScoped<IBeerDescriptionService, BeerDescriptionService>(); //! Register the BeerDescriptionService for dependency injection, specifying that whenever IBeerDescriptionService is needed, a BeerDescriptionService should be provided.

builder.Services.AddEndpointsApiExplorer(); // For support to API explorer
builder.Services.AddSwaggerGen(); // For support to Swagger


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection(); // Middleware to redirect HTTP requests to HTTPS
app.UseAuthorization(); //! Adding this line to use authorization middleware
app.MapControllers(); //! Adding this line to map attribute-routed controllers

// Sample data for the weather default API
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
