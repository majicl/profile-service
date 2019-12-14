using System;
namespace ProfileService.Domain.Users
{
    public class Customer
    {
        public Customer(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
    }
}
