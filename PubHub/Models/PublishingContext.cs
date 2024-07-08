using Microsoft.EntityFrameworkCore;

namespace PubHub.Models
{
    public class PublishingContext : DbContext
    {
        public PublishingContext(DbContextOptions<PublishingContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<PrintDistributor> PrintDistributors { get; set; }
        public DbSet<DistributorPublication> DistributorPublications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerID);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Subscriptions)
                .WithOne(s => s.Customer)
                .HasForeignKey(s => s.CustomerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Addresses)
                .WithOne(a => a.Customer)
                .HasForeignKey(a => a.CustomerID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Publication>()
                .HasKey(p => p.PublicationID);

            modelBuilder.Entity<Publication>()
                .HasMany(p => p.Subscriptions)
                .WithOne(s => s.Publication)
                .HasForeignKey(s => s.PublicationID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Publication>()
                .HasMany(p => p.DistributorPublications)
                .WithOne(dp => dp.Publication)
                .HasForeignKey(dp => dp.PublicationID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PrintDistributor>()
                .HasKey(pd => pd.DistributorID);

            modelBuilder.Entity<PrintDistributor>()
                .HasMany(pd => pd.DistributorPublications)
                .WithOne(dp => dp.Distributor)
                .HasForeignKey(dp => dp.DistributorID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DistributorPublication>()
                .HasKey(dp => dp.DistributorPublicationID);

            modelBuilder.Entity<Subscription>()
                .HasKey(s => s.SubscriptionID);

            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.Customer)
                .WithMany(c => c.Subscriptions)
                .HasForeignKey(s => s.CustomerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.Address)
                .WithMany()
                .HasForeignKey(s => s.AddressID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.Publication)
                .WithMany(p => p.Subscriptions)
                .HasForeignKey(s => s.PublicationID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
