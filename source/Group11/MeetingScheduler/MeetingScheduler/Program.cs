using MeetingScheduler.Data;
using MeetingScheduler.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Serilog;
using Syncfusion.Blazor;
using Utilities;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjc3MTE1NkAzMjMzMmUzMDJlMzBVcWdoMkM3TG1Mbnp6M1FzMCs2cTBFbklxcE5YQm5oV3VESGVZU1VXZjdvPQ==");
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
//builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5281/") });
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddSingleton<IUserHandler, UserHandler>();
builder.Services.AddScoped<IMeetingService, MeetingService>();
builder.Services.AddScoped<ILocalStorage, LocalStorage>();
builder.Services.AddScoped<IUserRegisterService, UserRegisterService>();
builder.Services.AddSingleton<IRoleHandler, RoleHandler>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddSingleton<IReportsHandler, ReportsHandler>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddSyncfusionBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
//app.UseSerilogRequestLogging();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
