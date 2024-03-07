<<<<<<< HEAD
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using BeerAPI.Validators;
using BeerAPI.Models;

=======
>>>>>>> 43236e633e5685f12917c77abfe8f4aaa33ca8ed
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
<<<<<<< HEAD
builder.Services.AddControllers(); //! Adding this line to add support for MVC controllers
builder.Services.AddFluentValidationAutoValidation(); //!  Register FluentValidation's automatic validation. This integrates FluentValidation into the ASP.NET Core pipeline.
builder.Services.AddTransient<IValidator<Beer>, BeerValidator>(); //! Register the BeerValidator for dependency injection, specifying that whenever IValidator<Beer> is needed, a BeerValidator should be provided.
builder.Services.AddEndpointsApiExplorer(); // For support to API explorer
builder.Services.AddSwaggerGen(); // For support to Swagger

=======
builder.Services.AddControllers(); //! Adding this line to add support for controllers
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
>>>>>>> 43236e633e5685f12917c77abfe8f4aaa33ca8ed

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

<<<<<<< HEAD

app.UseHttpsRedirection(); // Middleware to redirect HTTP requests to HTTPS
app.UseAuthorization(); //! Adding this line to use authorization middleware
app.MapControllers(); //! Adding this line to map attribute-routed controllers

// Sample data for the weather default API
=======
app.UseHttpsRedirection();
app.UseAuthorization(); //! Adding this line to use authorization middleware
app.MapControllers(); //! Adding this line to map attribute-routed controllers


>>>>>>> 43236e633e5685f12917c77abfe8f4aaa33ca8ed
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
