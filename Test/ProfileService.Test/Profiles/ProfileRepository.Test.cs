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
using System.Linq;

namespace ProfileService.Test.Profiles
{
    public class ProfileRepositoryTest
    {
        private DbContextOptions<DataContext> DbOptions = new DbContextOptionsBuilder<DataContext>()
             .UseInMemoryDatabase(databaseName: "Profiles").EnableSensitiveDataLogging().Options;

        [Fact]
        public async Task GetCustomerProfileByIdAsync_ReturnsNotNull()
        {
            using (var context = new DataContext(DbOptions))
            {
                // Arrange
                var guid = Guid.Parse("ab6816cb-6d5d-4bf2-af2f-e793ebf7af97");
                context.Profiles.RemoveRange(context.Profiles);
                context.Customers.RemoveRange(context.Customers);
                context.SaveChanges();
                context.Profiles.Add(new UserProfile(
                        "198512127996",
                        "x.x@gmail.com",
                        new MobilePhone { CountryCode = "46", Number = "070765342" },
                        new Domain.Users.Customer(guid))
                    );
                context.SaveChanges();

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

        [Fact]
        public async Task CreateAsync_DuplicateData_ThrowExpetion()
        {
            using (var context = new DataContext(DbOptions))
            {
                // Arrange
                var guid = Guid.Parse("ab6816cb-6d5d-4bf2-af2f-e793ebf7af97");
                context.Profiles.RemoveRange(context.Profiles);
                context.Customers.RemoveRange(context.Customers);
                context.SaveChanges();
                context.Profiles.Add(new UserProfile(
                        "198512127996",
                        "x.x@gmail.com",
                        new MobilePhone { CountryCode = "46", Number = "070765342" },
                        new Domain.Users.Customer(guid))
                    );
                context.SaveChanges();

                var _profileRepository = new ProfileRepository(context);

                // Act
                // Assert
                await Assert.ThrowsAsync<Exception>(
                () => _profileRepository.CreateAsync(new UserProfile(
                        "198512127996",
                        "x.x@gmail.com",
                        new MobilePhone { CountryCode = "46", Number = "070765342" },
                        new Domain.Users.Customer(guid))
                    , CancellationToken.None)
                );
            }
        }

        [Fact]
        public async Task CreateAsync_ReturnsGDOne()
        {
            using (var context = new DataContext(DbOptions))
            {
                // Arrange
                var guid = Guid.Parse("ab6816cb-6d5d-4bf2-af2f-e793ebf7af97");
                context.Profiles.RemoveRange(context.Profiles);
                context.Customers.RemoveRange(context.Customers);
                context.SaveChanges();

                var _profileRepository = new ProfileRepository(context);

                // Act
                var result = await _profileRepository.CreateAsync(new UserProfile(
                         "198512127996",
                         "x.x@gmail.com",
                         new MobilePhone { CountryCode = "46", Number = "070765342" },
                         new Domain.Users.Customer(guid))
                     , CancellationToken.None);


                // Assert
                Assert.True(result > 0);
            }
        }

        [Fact]
        public async Task ModifyAsync_Not_Existing_ThrowExpetion()
        {
            using (var context = new DataContext(DbOptions))
            {
                // Arrange
                context.Profiles.RemoveRange(context.Profiles);
                context.Customers.RemoveRange(context.Customers);
                context.SaveChanges();

                var _profileRepository = new ProfileRepository(context);

                // Act
                // Assert
                await Assert.ThrowsAsync<Exception>(
                () => _profileRepository.ModifyAsync(new UserProfile(
                        "198512127996",
                        "x.x@gmail.com",
                        new MobilePhone { CountryCode = "46", Number = "070765342" },
                        new Domain.Users.Customer(Guid.NewGuid()))
                    , CancellationToken.None)
                );
            }
        }

        [Fact]
        public async Task ModifyAsync_ReturnsGDOne()
        {
            using (var context = new DataContext(DbOptions))
            {
                // Arrange
                var guid = Guid.Parse("ab6816cb-6d5d-4bf2-af2f-e793ebf7af96");

                context.Profiles.RemoveRange(context.Profiles);
                context.Customers.RemoveRange(context.Customers);

                var profile = new UserProfile(
                        "198512127996",
                        "x.x@gmail.com",
                        new MobilePhone { CountryCode = "46", Number = "070765342" },
                        new Domain.Users.Customer(guid));
                context.Profiles.Add(profile);
                context.SaveChanges();

                var _profileRepository = new ProfileRepository(context);
                var profileId = await context.Profiles.Select(_ => _.Id).FirstAsync();
                profile.Modify("198512127997",  "hi@gmail.com", new MobilePhone { CountryCode="47", Number="90874532" }, profile.Customer, profileId);

                // Act
                var result = await _profileRepository.ModifyAsync(profile, CancellationToken.None);

                // Assert
                Assert.True(result > 0);
                Assert.Equal(profile.SSN , "198512127997");
                Assert.Equal(profile.Email, "hi@gmail.com");
            }
        }

        [Fact]
        public async Task DeleteAsync_Not_Existing_ThrowExpetion()
        {
            using (var context = new DataContext(DbOptions))
            {
                // Arrange
                context.Profiles.RemoveRange(context.Profiles);
                context.Customers.RemoveRange(context.Customers);
                context.SaveChanges();

                var _profileRepository = new ProfileRepository(context);

                // Act
                // Assert
                await Assert.ThrowsAsync<Exception>(
                () => _profileRepository.DeleteAsyncCustomerId(Guid.NewGuid() , CancellationToken.None));
            }
        }

        [Fact]
        public async Task DeleteAsync_ReturnsGDOne()
        {
            using (var context = new DataContext(DbOptions))
            {
                // Arrange
                var guid = Guid.Parse("ab6816cb-6d5d-4bf2-af2f-e793ebf7af96");

                context.Profiles.RemoveRange(context.Profiles);
                context.Customers.RemoveRange(context.Customers);

                var profile = new UserProfile(
                        "198512127996",
                        "x.x@gmail.com",
                        new MobilePhone { CountryCode = "46", Number = "070765342" },
                        new Domain.Users.Customer(guid));
                context.Profiles.Add(profile);
                context.SaveChanges();

                var _profileRepository = new ProfileRepository(context);

                // Act
                var result = await _profileRepository.DeleteAsyncCustomerId(guid, CancellationToken.None);

                // Assert
                Assert.True(result > 0);
            }
        }

    }
}
