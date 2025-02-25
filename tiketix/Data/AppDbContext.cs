using Microsoft.EntityFrameworkCore;
using tiketix.Models.Entities;

namespace tiketix.Data
{
    public class AppDbContext : DbContext
{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        }
        public DbSet<EventClient> EventClients { get; set; }
}
}

