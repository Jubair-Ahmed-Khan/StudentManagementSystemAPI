using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using STMS.Persistence.Contacts;
using STMS.Persistence.Data;
using STMS.Persistence.Repositories;
using STMS.Presentation.MiddleWares;
using STMS.Services.Mappers;
using STMS.Services.Services;
using System.Text;

namespace STMS.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            //Jwt configuration starts here
            var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
            var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option => {

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

            //REgister Database
            builder.Services.AddDbContext<StudentDbContext>(option => {
                option.UseNpgsql("Server=localhost;Database=StudentManagementSystem;Port=5432;User Id=postgres;Password=jubair3128@;");
            }
            );

            // Register the StudentMappers
            builder.Services.AddScoped<IStudentMappers, StudentMappers>();

            // Register other services
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddHostedService<NotificationWorkerService>();


            //Register Cors
            builder.Services.AddCors(options =>
            {
                var appSettings = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string>();
                var allowedOrigins = appSettings.Split(',');

                options.AddPolicy("NextApp", builder =>
                {
                    builder.WithOrigins(allowedOrigins)
                           .AllowAnyMethod() // Adjust methods (GET, POST, etc.) as needed
                           .AllowAnyHeader() // Adjust headers as needed
                           .AllowAnyOrigin(); // If using credentials
                });
            });

            builder.Services.AddResponseCaching();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            //Custom Middleware Starts
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseResponseCaching();
            //Custom Middleware Ends


            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            //cors used
            app.UseCors("NextApp");

            app.Run();
        }
    }
}
