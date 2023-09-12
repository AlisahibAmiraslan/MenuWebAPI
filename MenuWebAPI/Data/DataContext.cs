using MenuWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MenuWebAPI.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<HeaderMenu> HeaderMenus { get; set; }
        public DbSet<FooterMenu> FooterMenus { get; set; } 
        public DbSet<About> Abouts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
