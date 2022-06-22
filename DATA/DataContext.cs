
using Microsoft.EntityFrameworkCore;
using TestApi.Entities;

namespace TestApi.DATA
{
    public class ApplicationDbContext  : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext > options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("DataSource=datingapp.db");
        public DbSet<AppUser> users {get;set;}
    }
}