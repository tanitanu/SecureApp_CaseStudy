using DXC.BlogConnect.WebAPI.Data;
using DXC.BlogConnect.WebAPI.Models;
using DXC.BlogConnect.WebAPI.ServiceExtension;
using DXC.BlogConnect.WebAPI.Services;
using DXC.BlogConnect.WebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DXC.BlogConnect.WebAPI.Repositories.Interfaces;
using DXC.BlogConnect.WebAPI.Repositories;


/*
 * Created By: Kishore
 */
/* Updated By: Prabu Elavarasan*/
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDIServices(builder.Configuration);
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<ITokenHandlerService, TokenHandlerService>();
builder.Services.AddScoped<IBlogPostService, BlogPostService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

//JWT
builder.Services.AddSwaggerGen(options =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter a valid JWT bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securityScheme, new string[] {} }
    });
});


//DB Connection
builder.Services.AddDbContext<BlogConnectDbcontext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogConnectSQLConnection"));
});


//Automapper
builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Logging.ClearProviders();
builder.Logging.AddLog4Net("log4net.config");
//JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
