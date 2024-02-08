using BlazorCARS.HealthShield.WebApp.Services.IServices;
using BlazorCARS.HealthShield.WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//Service registration

builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddHttpClient<IStateService, StateService>();
builder.Services.AddHttpClient<IVaccineService, VaccineService>();
builder.Services.AddHttpClient<ICountryService, CountryService>();
builder.Services.AddHttpClient<IUserRoleSerivce, UserRoleSerivce>();
builder.Services.AddHttpClient<IHospitalService, HospitalService>();
builder.Services.AddHttpClient<IRecipientService, RecipientService>();
builder.Services.AddHttpClient<IHospitalRegistrySerivce, HospitalRegistrySerivce>();
builder.Services.AddHttpClient<IRecipientRegistrySerivce, RecipientRegistrySerivce>();
builder.Services.AddHttpClient<IVaccineScheduleService, VaccineScheduleService>();
builder.Services.AddHttpClient<IVaccineRegistrationService, VaccineRegistrationService>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddScoped<IVaccineService, VaccineService>();
builder.Services.AddScoped<ICountryService, CountryService>(); 
builder.Services.AddScoped<IUserRoleSerivce, UserRoleSerivce>();
builder.Services.AddScoped<IHospitalService, HospitalService>();
builder.Services.AddScoped<IRecipientService, RecipientService>();
builder.Services.AddScoped<IHospitalRegistrySerivce, HospitalRegistrySerivce>();
builder.Services.AddScoped<IRecipientRegistrySerivce, RecipientRegistrySerivce>();
builder.Services.AddScoped<IVaccineScheduleService, VaccineScheduleService>();
builder.Services.AddScoped<IVaccineRegistrationService, VaccineRegistrationService>();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
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

app.UseSession();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
