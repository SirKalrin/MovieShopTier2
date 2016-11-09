using System.Collections.Generic;

namespace ServiceGateway.Entities
{
    public class Customer : AbstractEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public string Email { get; set; }
        public List<Order> Orders { get; set; }
    }
}
