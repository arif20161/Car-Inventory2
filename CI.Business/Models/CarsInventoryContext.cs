using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using CI.Business.Models.Mapping;

namespace CI.Business.Models
{
    public partial class CarsInventoryContext : DbContext
    {
        static CarsInventoryContext()
        {
            Database.SetInitializer<CarsInventoryContext>(null);
        }

        public CarsInventoryContext()
            : base("Name=CarsInventoryContext")
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CarMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
