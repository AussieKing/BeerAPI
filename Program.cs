using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using BeerAPI.Validators;
using BeerAPI.Models;
using BeerAPI.Services; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); 
builder.Services.AddFluentValidationAutoValidation(); 
builder.Services.AddTransient<IValidator<Beer>, BeerValidator>(); 
builder.Services.AddScoped<IBeerDescriptionService, BeerDescriptionService>();
builder.Services.AddScoped<IBeerService, BeerService>();
builder.Services.AddScoped<ITrolleyService, TrolleyService>();

builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); 
app.UseAuthorization();
app.MapControllers(); 

app.Run();
