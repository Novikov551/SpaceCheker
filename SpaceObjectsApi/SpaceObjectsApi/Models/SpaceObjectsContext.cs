using Microsoft.EntityFrameworkCore;

namespace SpaceObjectsApi.Models
{
    public class SpaceObjectsContext : DbContext
    {
        public virtual DbSet<Planet> Planets { get; set; }

        public virtual DbSet<Star> Stars { get; set; }

        public virtual DbSet<Asteroid> Asteroids { get; set; }

        public virtual DbSet<BlackHole> BlackHoles { get; set; }

        public SpaceObjectsContext(DbContextOptions<SpaceObjectsContext> options)
            : base(options)
        { 
            Database.EnsureCreated(); 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             
        }
    }
}
