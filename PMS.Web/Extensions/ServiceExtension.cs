using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PMS.Web.Configuration;
using PMS.Web.Data;
using PMS.Web.Entities;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMS.Web.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            
        }
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("ApiCorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
        }
        public static void ConfigureSqlServer(this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(config["ConnectionStrings:DefaultConnection"]);
            });
        }
        public static void ConfigureJwt(this IServiceCollection services,IConfiguration config)
        {
            services.Configure<JwtConfig>(config.GetSection("JwtConfig"));
           
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppUserRoles>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

        }
        public static void ConfigureJwtAuth(this IServiceCollection services,IConfiguration config)
        {
            var tokenValidationParams = new TokenValidationParameters
            {

                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = config["JwtConfig:ValidAudience"],
                ValidIssuer = config["JwtConfig:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtConfig:Secret"]))
            };
            services.AddSingleton(tokenValidationParams);
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt => {
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = tokenValidationParams;
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PMS.Web", Version = "v1" });
                c.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme.ToLowerInvariant(),
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.OperationFilter<AuthResponsesOperationFilter>();
            });
        }
    }
    internal class AuthResponsesOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var attributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                                .Union(context.MethodInfo.GetCustomAttributes(true));

            if (attributes.OfType<IAllowAnonymous>().Any())
            {
                return;
            }

            var authAttributes = attributes.OfType<IAuthorizeData>();

            if (authAttributes.Any())
            {
                operation.Responses["401"] = new OpenApiResponse { Description = "Unauthorized" };

                if (authAttributes.Any(att => !String.IsNullOrWhiteSpace(att.Roles) || !String.IsNullOrWhiteSpace(att.Policy)))
                {
                    operation.Responses["403"] = new OpenApiResponse { Description = "Forbidden" };
                }

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "BearerAuth",
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            Array.Empty<string>()
                        }
                    }
                };
            }
        }
    }
}
