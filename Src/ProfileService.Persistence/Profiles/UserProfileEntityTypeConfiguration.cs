using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfileService.Domain.Profiles;
using ProfileService.Domain.Users;

namespace ProfileService.Persistence.Profiles
{
    public class UserProfileEntityTypeConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(b => b.Id);

            builder
                .Property(_ => _.SSN)
                .HasMaxLength(12)
                .IsUnicode()
                .IsRequired();

            builder
                .Property(_ => _.Email)
                .HasMaxLength(100);

            builder
                .OwnsOne(o => o.MobilePhone)
                .Property(_ => _.Number)
                .HasMaxLength(10);

            builder
                .OwnsOne(o => o.MobilePhone)
                .Property(_ => _.CountryCode)
                .HasMaxLength(4);

            builder
                .HasOne(_ => _.Customer)
                .WithOne(_=> _.Profile)
                .HasForeignKey<Customer>(e => e.Id); 


        }
    }
}
