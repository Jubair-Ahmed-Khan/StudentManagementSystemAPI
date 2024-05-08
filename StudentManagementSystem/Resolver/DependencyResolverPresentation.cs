using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using STMS.Persistence.Resolver;
//using STMS.Services.Mappers;
using STMS.Services.Resolver;
//using STMS.Services.Services;
using System.Text;

namespace STMS.Presentation.Resolver
{
    public static class DependencyResolverPresentation
    {
        public static IServiceCollection Register(this IServiceCollection services,IConfiguration configuration)
        {
            DependencyResolverService.Register(services, configuration);

            var jwtIssuer = configuration.GetSection("Jwt:Issuer").Get<string>();
            var jwtKey = configuration.GetSection("Jwt:Key").Get<string>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = jwtIssuer,
                      ValidAudience = jwtIssuer,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                  };
              });
            //Jwt configuration ends here


            // Add services to the container.
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(option => {

                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            services.AddCors(options =>
            {
                var appSettings = configuration.GetSection("Cors:AllowedOrigins").Get<string>();
                var allowedOrigins = appSettings.Split(',');

                options.AddPolicy("NextApp", builder =>
                {
                    builder.WithOrigins(allowedOrigins)
                           .AllowAnyMethod() // Adjust methods (GET, POST, etc.) as needed
                           .AllowAnyHeader() // Adjust headers as needed
                           .AllowAnyOrigin(); // If using credentials
                });
            });

            services.AddResponseCaching();

            return services;

        }
    }
}
