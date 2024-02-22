using Microsoft.EntityFrameworkCore;
using Banking1.Models;
namespace Banking1.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<CustomerMaster> CustomerMaster { get; set; }
        public DbSet<Banking1.Models.CustomerBalance> CustomerBalance { get; set; }
        public DbSet<Banking1.Models.CustomerPoint> CustomerPoint { get; set; }
    }
   
}
