using System;
using System.Threading.Tasks;

namespace ProfileService.Domain.Profiles
{
    public interface IProfileRepository
    {
        Task<UserProfile> GetCustomerProfileByIdAsync(Guid id);
        Task<int> DeleteAsyncCustomerId(Guid id);
        Task<int> CreateAsync(UserProfile userProfile);
        Task<int> ModifyAsync(UserProfile userProfile);
    }
}
