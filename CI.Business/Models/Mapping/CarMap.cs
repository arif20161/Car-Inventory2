using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CI.Business.Models.Mapping
{
    public class CarMap : EntityTypeConfiguration<Car>
    {
        public CarMap()
        {
            // Primary Key
            this.HasKey(t => t.CarId);

            // Properties
            this.Property(t => t.Brand)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Model)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Car");
            this.Property(t => t.CarId).HasColumnName("CarId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Brand).HasColumnName("Brand");
            this.Property(t => t.Model).HasColumnName("Model");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.New).HasColumnName("New");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Cars)
                .HasForeignKey(d => d.UserId);

        }
    }
}
