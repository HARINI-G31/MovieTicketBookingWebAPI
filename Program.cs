
using Microsoft.EntityFrameworkCore;
using MovieTicketBookingWebAPI.Data;
using MovieTicketBookingWebAPI.Repositories;
using MovieTicketBookingWebAPI.Repositories.Interfaces;
using MovieTicketBookingWebAPI.Services;
using MovieTicketBookingWebAPI.Services.Interfaces;
var builder = WebApplication.CreateBuilder(args);


// Database connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Movie Repository
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

// Movie Service
builder.Services.AddScoped<IMovieService, MovieService>();

builder.Services.AddScoped<ITheatreRepository, TheatreRepository>();

builder.Services.AddScoped<ITheatreService, TheatreService>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
