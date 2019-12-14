using System;
using ProfileService.Domain.Users;

namespace ProfileService.Domain.Profiles
{
    public class UserProfile
    {
        public Guid Id { get; private set; }

        public Customer Customer { get; private set; }

        public string SSN { get; private set; }
        public string Email { get; private set; }
        public MobilePhone MobilePhone { get; private set; }

        public UserProfile(string _SSN, string email, MobilePhone mobilePhone, Customer customer)
        {
            SSN = _SSN;
            Email = email;
            MobilePhone = mobilePhone;
            Customer = customer;
        }
    }
}
