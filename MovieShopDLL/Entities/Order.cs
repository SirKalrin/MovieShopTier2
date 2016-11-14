using System;

namespace ServiceGateway.Entities
{
    public class Order : AbstractEntity
    {

        public DateTime DateTime { get; set; }
        public Customer Customer { get; set; }
        public Movie Movie { get; set; }
    }
}
