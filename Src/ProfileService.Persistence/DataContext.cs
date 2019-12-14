using System;
using Microsoft.EntityFrameworkCore;
using ProfileService.Domain.Profiles;
using ProfileService.Persistence.Profiles;

namespace ProfileService.Persistence
{
    public class DataContext : DbContext
    {
        public DbSet<UserProfile> Profiles { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserProfileEntityTypeConfiguration());
        }
    }
}
