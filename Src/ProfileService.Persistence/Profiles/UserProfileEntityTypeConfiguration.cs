using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfileService.Domain.Profiles;

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
             .Property(_ => _.MobilePhone.Number)
             .HasMaxLength(10);

            builder
               .Property(_ => _.MobilePhone.CountryCode)
               .HasMaxLength(4);
        }
    }
}
