using Microsoft.EntityFrameworkCore;
using STMS.Persistence.Models;

namespace STMS.Persistence.Data
{
    public class StudentDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {

        }
    }  
}
