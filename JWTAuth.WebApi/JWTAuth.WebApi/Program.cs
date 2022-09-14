using JWTAuth.WebApi.Interface;
using JWTAuth.WebApi.Models;
using JWTAuth.WebApi.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

/**
 * [IMPORTANTE]
 * Se agrega autenticación de JWT
 */
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    /**
     * We configured authorization middleware in the startup. Here we have passed the security key
     * when creating the token and enabled validation of Issuer and Audience. Also, we have set “SaveToken” to true,
     * which stores the bearer token in HTTP Context. So we can use the token later in the controller.
     */
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

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
/**Se agrega autenticación para JWT*/
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
