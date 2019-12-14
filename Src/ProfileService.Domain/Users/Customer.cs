using System;
using ProfileService.Domain.Profiles;

namespace ProfileService.Domain.Users
{
    public class Customer
    {
        public Customer(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
        public UserProfile Profile { get; private set; }
    }
}
