using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using STMS.Persistence.Contacts;

//using STMS.Persistence.Contacts;
using STMS.Persistence.Data;
using STMS.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STMS.Persistence.Resolver
{
    public static class DependecnyResolverPersistence
    {
        public static IServiceCollection Register(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddDbContext<PostgresDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Postgres")));
            services.AddScoped<IStudentRepository, StudentRepository>();
            
            return services;
        }
    }
}
