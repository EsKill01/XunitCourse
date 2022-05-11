using Microsoft.EntityFrameworkCore;
using MVCLibraryApp.Data.Models;

namespace MVCLibraryApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
    }
}
