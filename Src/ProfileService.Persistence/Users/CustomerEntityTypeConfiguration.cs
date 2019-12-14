using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfileService.Domain.Profiles;
using ProfileService.Domain.Users;

namespace ProfileService.Persistence.Profiles
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(b => b.Id);

            builder
               .HasOne(_ => _.Profile)
               .WithOne(_ => _.Customer)
               .HasForeignKey<UserProfile>(e => e.Id);
        }
    }
}
