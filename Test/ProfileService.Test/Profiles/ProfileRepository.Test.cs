using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProfileService.Domain.Profiles;
using ProfileService.Persistence;
using ProfileService.Persistence.Profiles;
using Xunit;

namespace ProfileService.Test.Profiles
{
    public class ProfileRepositoryTest
    {
        public ProfileRepositoryTest()
        {
            GenerateFakeData();
        }

        private DbContextOptions<DataContext> DbOptions = new DbContextOptionsBuilder<DataContext>()
             .UseInMemoryDatabase(databaseName: "Profiles").Options;

        private Guid guid = Guid.Parse("ab6816cb-6d5d-4bf2-af2f-e793ebf7af97");
        private void GenerateFakeData()
        {
            // Insert seed data into the database using one instance of the context
            using (var context = new DataContext(DbOptions))
            {
                context.Profiles.Add(new UserProfile(
                        "198512127996",
                        "x.x@gmail.com",
                        new MobilePhone { CountryCode = "46", Number = "070765342" },
                        new Domain.Users.Customer(guid))
                    );
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task GetCustomerProfileByIdAsync_ReturnsNotNull()
        {
            using (var context = new DataContext(DbOptions))
            {
                // Arrange
                var _profileRepository = new ProfileRepository(context);
                // Act
                var result = await _profileRepository.GetCustomerProfileByIdAsync(guid, CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.True(result.SSN == "198512127996");
                Assert.True(result.Email == "x.x@gmail.com");
                Assert.True(result.MobilePhone.CountryCode == "46");
                Assert.True(result.MobilePhone.Number == "070765342");
            }
        }
    }
}
