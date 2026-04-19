using Microsoft.EntityFrameworkCore;

namespace FinJourney.Shared
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


    }
}
