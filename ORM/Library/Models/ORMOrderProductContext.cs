using Microsoft.EntityFrameworkCore;

namespace ORMLibrary.Models
{
    public class ORMOrderProductContext : DbContext
    {
        public ORMOrderProductContext()
        {
        }

        public ORMOrderProductContext(DbContextOptions<ORMOrderProductContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ORMOrder> Orders { get; set; }
        public virtual DbSet<ORMProduct> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(Localdb)\\MSSQLLocalDB;Initial Catalog=ADO.NET_DB;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ORMOrder>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasColumnType("int");
            });

            modelBuilder.Entity<ORMProduct>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });
        }
    }
}
