using Microsoft.EntityFrameworkCore;
using backend_dotnet.Models;

namespace backend_dotnet.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<NetworkScan> NetworkScans { get; set; }    
    }
}

//this file Creates Database Context from the database we created via pgadmin 4
//_context is used to save and read data
