using DXC.BlogConnect.WebApp.Services;
using DXC.BlogConnect.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Configuration;
/*
* Created By: Kishore
*/
namespace DXC.BlogConnect.WebApp.ServiceExtension
{
    public static class ServiceExtension
    {

        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            /*
             * Created By: Kishore start
             */
            
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(configuration.GetValue<string>("ServiceUrl:ApiUrl"))});
            services.AddScoped(typeof(IUserService), typeof(UserService));
            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            services.AddScoped(typeof(IBlogService), typeof(BlogService));

            services.AddMvc();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, cookieAuthOptions =>
                {
                    cookieAuthOptions.Cookie.Name = "BlogConnectCookie";
                    cookieAuthOptions.LoginPath = "/login";
                    cookieAuthOptions.LogoutPath = "/logOut";
                    cookieAuthOptions.AccessDeniedPath = "/accessDenied";
                });

            return services;
        }
        }
        
    }
