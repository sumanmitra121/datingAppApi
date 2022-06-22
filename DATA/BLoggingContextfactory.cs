
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TestApi.DATA
{
    public class BLoggingContextfactory 
    : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args){
            IConfigurationRoot _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.Development.json").Build();
            var _builder = new DbContextOptionsBuilder<ApplicationDbContext>();
               var _con = _config.GetConnectionString("DefaultConnection");
               _builder.UseSqlite(_con);
               return new ApplicationDbContext(_builder.Options);
        }
    }
}