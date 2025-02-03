using Microsoft.EntityFrameworkCore;
using EnergyScore.Persistence.DBConnection;
using EnergyScore.Application.Operations;
using EnergyScore.Application.Mappers;
using EnergyScore.Application.HPXMLConversion;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DBConnection
builder.Services.AddDbContext<DbConnect>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//AutoMapperConfig
builder.Services.AddAutoMapper(typeof(MapConfig));

//Operations
builder.Services.AddScoped<IAddressOperations, AddressOperations>();
builder.Services.AddScoped<IHPXMLOperations, HPXMLOperations>();


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
