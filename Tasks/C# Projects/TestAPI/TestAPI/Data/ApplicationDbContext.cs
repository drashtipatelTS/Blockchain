using Microsoft.EntityFrameworkCore;
using TestAPI.Model;

namespace TestAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        { }   
        public DbSet<StudentEntity> StudentRegister { get; set; }
    }
}
