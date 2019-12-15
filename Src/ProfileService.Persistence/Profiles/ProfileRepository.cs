using System;
using System.Threading;
using System.Threading.Tasks;
using ProfileService.Domain.Profiles;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProfileService.Persistence.Profiles
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly DataContext _context;
        public ProfileRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> CreateAsync(UserProfile userProfile, CancellationToken cancellationToken)
        {
            var duplicated = await _context.Profiles
                  .Where(x => x.Customer.Id == userProfile.Customer.Id).AnyAsync(cancellationToken);

            if (duplicated)
            {
                throw new Exception($"The customer's profile with ID: {userProfile.Customer.Id} is already exist.");
            }

            await _context.Profiles.AddAsync(userProfile, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> DeleteAsyncCustomerId(Guid id, CancellationToken cancellationToken)
        {
            var profileExist = await _context.Profiles
                  .Where(x => x.Customer.Id == id).AnyAsync(cancellationToken);

            if (profileExist == false)
            {
                throw new Exception($"There is no customer's profile with ID: {id}.");
            }

            _context.Profiles.Remove(_context.Profiles.Find(id));
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<UserProfile> GetCustomerProfileByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Profiles
                 .Where(x => x.Customer.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> ModifyAsync(UserProfile userProfile, CancellationToken cancellationToken)
        {
            var profileExist = await _context.Profiles
                .Where(x => x.Customer.Id == userProfile.Customer.Id).AnyAsync(cancellationToken);

            if (profileExist == false)
            {
                throw new Exception($"The customer's profile with ID: {userProfile.Customer.Id} not found.");
            }

            _context.Entry(userProfile).State = EntityState.Modified;
            _context.Profiles.Update(userProfile);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
