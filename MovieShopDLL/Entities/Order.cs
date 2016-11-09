using System;

namespace ServiceGateway.Entities
{
    public class Order : AbstractEntity
    {

        public DateTime DateTime { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
