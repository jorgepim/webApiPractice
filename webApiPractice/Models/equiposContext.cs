using Microsoft.EntityFrameworkCore;
namespace webApiPractice.Models
{
    public class equiposContext : DbContext
    {
        public equiposContext(DbContextOptions<equiposContext> options) : base(options) 
        {
            
        }
        public DbSet<equipos> equipos { get; set; }
    }
}
