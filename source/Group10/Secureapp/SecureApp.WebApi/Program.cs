using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Net.Http.Headers;
using SecureApp.Model;
using SecureApp.Service;
using System;
using AuthenticationService = SecureApp.Service.AuthenticationService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<SecureAppContext>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<SecureAppContext>()
            .AddDefaultTokenProviders();

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
app.UseCors(policy => policy.WithOrigins("http://localhost:44360", "https://localhost:44360",
    "http://localhost:7257", "https://localhost:7257")
.AllowAnyMethod()
.WithHeaders(HeaderNames.ContentType));

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
