using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.Contracts.Persistence;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Application.Contracts.ServiceInterfaces;
using System.Security.Claims;
using Domain.Entities.Concrete;

namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString")));

            //services.AddIdentity<User, ApplicationRole>(options =>
            //{
            //    // Password settings
            //    options.Password.RequireDigit = true;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequiredLength = 6; // Set your desired minimum length
            //    options.Password.RequiredUniqueChars = 0;
            //})
            ////.AddEntityFrameworkStores<ApplicationDbContext>()
            //.AddUserStore<CustomUserStore>()
            //.AddDefaultTokenProviders();

            // Register the IPasswordHasher

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();


            MultipleAuthenticationAndAuthorzation(services, configuration);


            services.AddControllersWithViews();

            InjectInterfaces(services);

            return services;

        }



        public static void MultipleAuthenticationAndAuthorzation(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication()
            .AddJwtBearer("SimpleUsersBearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:secretKey"])),


                };
            })
            .AddJwtBearer("SuperUsersBearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:superadmin_secretKey"])),


                };

            });



            services.AddAuthorization(options =>
            {
                options.AddPolicy("SimpleUsersPolicy", policy =>
                        {
                            policy.RequireAuthenticatedUser();
                            policy.AuthenticationSchemes.Add("SimpleUsersBearer");
                           
                        });
            options.AddPolicy("SuperUsersPolicy", policy =>
                    policy.RequireAuthenticatedUser().AddAuthenticationSchemes("SuperUsersBearer"));
                options.AddPolicy("CombinedPolicy", policy =>
                    policy.RequireAuthenticatedUser().AddAuthenticationSchemes("SimpleUsersBearer", "SuperUsersBearer"));
            });




        }







        private static void InjectInterfaces(IServiceCollection services)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

          
            services.AddScoped<IJobSeekerRepository, JobSeekerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<PermissionService>();

            services.AddSingleton<TokenService>();
        }
    }
}
