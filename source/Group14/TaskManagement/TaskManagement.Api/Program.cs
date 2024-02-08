#region [Using]
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using TaskManagement.Api.Data;
using TaskManagement.Api.Mappings;
using TaskManagement.Api.MiddleWares;
using TaskManagement.Api.Repositories;
#endregion [Using]

#region [Summary]
///<author>sayyad, shaheena</author>
///<date>01-Nov-2023</date>
///<project>TaskManagement.Api</project>
///<class>Program</class>
/// <summary>
/// This is the main program
/// History
///     02-Nov-2023: Poornima Shanbhag: Updated for logger, Repository and AutoMapper
///     22-Nov-2023: sayyad, shaheena: Updates Logging and exception handler
/// </summary>
#endregion [Summary]

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add logger
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    //.WriteTo.Console(new CompactJsonFormatter())
    .WriteTo.File("Log/TaskManagment_Log.txt", rollingInterval: RollingInterval.Minute)

    .CreateLogger();
Log.Logger.Information("Logging is working fine");

builder.Host.UseSerilog();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();



// Add Task DB Context
builder.Services.AddDbContext<TaskDBContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("Task_DB")));

// Add Repository for Task
builder.Services.AddScoped<ITaskRepository, SQLTaskRepository>();

// Add Auto Mapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

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

app.UseMiddleware<ExceptionHandlerMiddleWare>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
