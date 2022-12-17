using Microsoft.EntityFrameworkCore;
using TestAppWeb.Models;

namespace TestAppWeb.Data
{
    public class ApplicationDbContext : DbContext // inherit from the entity framework core
    { 
        // create constructor - ctor double tab
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options) // configure DbContextOptions and pass those options in base class
        {

        }
        public DbSet<Category> Categories { get; set; } // create data table using entity framework and setup getters and setters
    }
}
