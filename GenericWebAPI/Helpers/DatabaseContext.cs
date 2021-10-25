using GenericWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GenericWebAPI.Helpers
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }

        public DbSet<CarProducer> CarProducers { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().HasKey(x => x.Guid);

            modelBuilder.Entity<CarProducer>().HasKey(x => x.Guid);

            modelBuilder.Entity<Car>().HasOne(x => x.Producer).WithMany(x => x.Cars).HasForeignKey(x => x.ProducerGuid);
        }
    }
}
