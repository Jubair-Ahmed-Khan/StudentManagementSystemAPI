using Microsoft.EntityFrameworkCore;
using STMS.Persistence.Models;

namespace STMS.Persistence.Data
{
    public class PostgresDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options):base(options)
        {

        }
    }
}
