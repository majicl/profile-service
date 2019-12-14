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

        public UserProfile() { }
        public UserProfile(string _SSN, string email, MobilePhone mobilePhone, Customer customer, Guid id = default(Guid))
        {
            Modify(_SSN, email, mobilePhone, customer, id);
        }

        public void Modify(string _SSN, string email, MobilePhone mobilePhone, Customer customer, Guid id = default(Guid))
        {
            SSN = _SSN;
            Email = email;
            MobilePhone = mobilePhone;
            Customer = customer;
            Id = id;
        }
    }
}
