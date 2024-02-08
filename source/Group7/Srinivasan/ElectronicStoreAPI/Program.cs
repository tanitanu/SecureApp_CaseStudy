using ElectronicStoreAPI.Data;
using ElectronicStoreAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//by Srinivasan
builder.Services.AddDbContext<ElecStoreDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EStoreConnStr")));

builder.Services.AddScoped<ICategoryRepository, SQLCategoryRepository>();

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
