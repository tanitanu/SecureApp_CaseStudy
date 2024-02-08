
using MeetingScheduler.DAL;
using MeetingScheduler.Model;
using Microsoft.Net.Http.Headers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IUserHandler, UserHandler>();
builder.Services.AddSingleton<IMeetingHandler, MeetingHandler>();
builder.Services.AddSingleton<IMeetingDAL, MeetingDAL>();
builder.Services.AddSingleton<IResourceHandler, ResourceHandler>();
builder.Services.AddSingleton<IResourceDAL, ResourceDAL>();
builder.Services.AddSingleton<IReportsDAL, ReportsDAL>();
builder.Services.AddSingleton<IReportsHandler, ReportsHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(policy=>policy.WithOrigins("http://localhost:44360", "https://localhost:44360", 
    "http://localhost:7257", "https://localhost:7257")
.AllowAnyMethod()
.WithHeaders(HeaderNames.ContentType));


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
