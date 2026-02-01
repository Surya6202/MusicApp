using MusicApp.Models;
using System.Data.Entity;

namespace MusicApp.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("AppDbContext") { }

        public DbSet<Music> MusicDetails { get; set; }
        public DbSet<User> Users { get; set; }
    }
}