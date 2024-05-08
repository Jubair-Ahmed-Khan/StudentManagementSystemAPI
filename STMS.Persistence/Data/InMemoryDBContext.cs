using Microsoft.EntityFrameworkCore;
using STMS.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STMS.Persistence.Data
{
    public class InMemoryDBContext :DbContext
    {
        public DbSet<Student> Students { get; set; }
        public InMemoryDBContext(DbContextOptions<PostgresDbContext> options):base(options)
        {

        }
    }
}
