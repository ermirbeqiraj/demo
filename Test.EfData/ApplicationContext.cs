using Microsoft.EntityFrameworkCore;

namespace Test.EfData
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Balance> Balances { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>(x =>
            {
                x.HasKey(x => x.Id);
                x.Property(x => x.Id).ValueGeneratedNever();
            });

            builder.Entity<Balance>(x =>
            {
                x.HasKey(x => x.CustomerId);
                x.Property(x => x.CustomerId).IsRequired();

                x.Property(x => x.Amount).IsRequired().IsConcurrencyToken();

                x.HasOne(x => x.Customer).WithOne(f => f.Balance).HasForeignKey<Balance>(fk => fk.CustomerId).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
