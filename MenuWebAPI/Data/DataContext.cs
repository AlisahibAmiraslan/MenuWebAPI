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
        public DbSet<Character>Characters { get; set; }
        public DbSet<Backpack> Backpacks { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Faction> Factions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }    
    }
}
