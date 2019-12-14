using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProfileService.Domain.Profiles
{
    public interface IProfileRepository
    {
        Task<UserProfile> GetCustomerProfileByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<int> DeleteAsyncCustomerId(Guid id, CancellationToken cancellationToken);
        Task<int> CreateAsync(UserProfile userProfile, CancellationToken cancellationToken);
        Task<int> ModifyAsync(UserProfile userProfile, CancellationToken cancellationToken);
    }
}
