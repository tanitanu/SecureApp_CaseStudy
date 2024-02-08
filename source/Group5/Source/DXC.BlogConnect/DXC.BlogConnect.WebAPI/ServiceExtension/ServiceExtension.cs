using DXC.BlogConnect.WebAPI.Data;
using DXC.BlogConnect.WebAPI.Data.FluentValidation;
using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.DTO;
using DXC.BlogConnect.WebAPI.Repositories;
using DXC.BlogConnect.WebAPI.Repositories.Interfaces;
using FluentValidation;
using DXC.BlogConnect.WebAPI.Models.DTO;
/*
* Created By: Kishore
*/
namespace DXC.BlogConnect.WebAPI.ServiceExtension
{
    //registering DI services, and configure that inside the Program.cs
    public static class ServiceExtension
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            /*
             * Created By: Kishore start
             */
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<ITokenHandlerRepository, TokenHandlerRepository>();
            services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            services.AddScoped<IBlogPostLikeRepository, BlogPostLikeRepository>();
            services.AddScoped<IBlogPostCommentRepository, BlogPostCommentRepository>();
            services.AddScoped<IImageRepository, ImageRepositoryCloudinary>();


            services.AddScoped<IValidator<UserDTO>, UserValidator>();
            services.AddScoped<IValidator<UserLoginDTO>, UserLoginValidator>();
            services.AddScoped<IValidator<BlogPostAddDTO>, BlogPostValidator>();
            services.AddScoped<IValidator<RoleEditDTO>, RoleEditValidator>();
            services.AddScoped<IValidator<RoleAddDTO>, RoleAddValidator>();

            /*
             * Created By: Kishore end
            */

            return services;
        }
    }
}
