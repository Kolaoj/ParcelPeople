using Microsoft.EntityFrameworkCore;
using ParcelPeople.Application.Services;
using ParcelPeople.Application.Services.Interfaces;
using ParcelPeople.Infrastructure.DbContexts;
using ParcelPeople.Infrastructure.Repositories;
using ParcelPeople.Infrastructure.Repositories.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
 c.EnableAnnotations());

var relativeConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Could not obtain the connection string, check appsettings"); 
var absoluteConnectionString = Path.GetFullPath(relativeConnectionString, AppContext.BaseDirectory);

builder.Services.AddDbContext<ShipmentDbContext>(options=> options.UseSqlite($"Data Source={absoluteConnectionString}"));

builder.Services.AddScoped<IShipmentService, ShipmentService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IParcelService, ParcelService>();

builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IParcelRepository, ParcelRepository>();

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
