using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using STMS.Persistence.Contacts;
using STMS.Persistence.Repositories;
using STMS.Persistence.Resolver;
using STMS.Services.Mappers;
using STMS.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STMS.Services.Resolver
{
    public static class DependencyResolverService
    {
        public static IServiceCollection Register(this IServiceCollection services,IConfiguration configuration)
        {
            DependecnyResolverPersistence.Register(services,configuration);

            services.AddScoped<IStudentMappers, StudentMappers>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddHostedService<NotificationWorkerService>();

            return services;
            
        }
    }
}
