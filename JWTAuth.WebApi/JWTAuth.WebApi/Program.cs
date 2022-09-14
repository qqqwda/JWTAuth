using JWTAuth.WebApi.Interface;
using JWTAuth.WebApi.Models;
using JWTAuth.WebApi.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add logger
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});


/**
 * [IMPORTANT]
 * Connection String fron appsettings.json
 * name of 'ConnectionStrings' must be PLURAL or it will throw null
 */
var connectionString = builder.Configuration.GetConnectionString("dbConnection");


// Add services to the container.

//Donot forgot to add ConnectionStrings as "dbConnection" to the appsetting.json file
builder.Services.AddDbContext<DatabaseContext>
    (options => options.UseSqlServer(connectionString));
builder.Services.AddTransient<IEmployees, EmployeeRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
