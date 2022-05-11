
using AutoMapper;
using BackCore.BLL.Settings;
using BackCore.Data;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BackCore.API.Extenstions
{
    public static class ServiceExtensions
    {

        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Inventory API";
                    document.Info.Description = "";
                    document.Info.TermsOfService = "None";

                };

                config.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT Token"));
                config.AddSecurity("JWT Token", Enumerable.Empty<string>(),
                    new NSwag.OpenApiSecurityScheme()
                    {
                        Type = OpenApiSecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        In = OpenApiSecurityApiKeyLocation.Header,
                        Description = "Copy this into the value field: Bearer {token}"
                    }
                );
            });
        
        }

        public static void AddCorsExtension(this IServiceCollection services)
        {
            services.AddCors(options =>
            {

                options.AddPolicy("CorsPolicy",
                  builder => builder
                  .AllowAnyMethod()
                  .AllowAnyHeader().AllowCredentials()
                  .SetIsOriginAllowed((x) => true));

            });
        }


        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
        

            IdentityBuilder builder = services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 3;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedPhoneNumber = true;
            });

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(options =>
             {
             options.SaveToken = true;
             options.RequireHttpsMetadata = false;
             options.TokenValidationParameters = new TokenValidationParameters()
             {
                 ValidateIssuerSigningKey = true,
                 ValidateIssuer = true,
                 ValidateAudience = true,
                 ValidateLifetime = true,
                 ClockSkew = TimeSpan.Zero,
                 ValidIssuer = configuration["JWTSettings:Issuer"],
                 ValidAudience = configuration["JWTSettings:Audience"],
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
             };

           });

        }

        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            
        }

    }
}
