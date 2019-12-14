using System;
using Microsoft.EntityFrameworkCore;
using ProfileService.Domain.Profiles;
using ProfileService.Domain.Users;
using ProfileService.Persistence.Profiles;

namespace ProfileService.Persistence
{
    public class DataContext : DbContext
    {
        public DbSet<UserProfile> Profiles { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DataContext() { }
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserProfileEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
        }
    }
}
