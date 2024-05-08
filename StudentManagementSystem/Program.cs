using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using STMS.Persistence.Contacts;
using STMS.Persistence.Data;
using STMS.Persistence.Repositories;
using STMS.Persistence.Resolver;
using STMS.Presentation.MiddleWares;
using STMS.Presentation.Resolver;
using STMS.Services.Mappers;
using STMS.Services.Resolver;
using STMS.Services.Services;
using System.Text;

namespace STMS.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            DependencyResolverPresentation.Register(builder.Services,builder.Configuration);

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
