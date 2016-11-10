using System.Collections.Generic;

namespace ServiceGateway.Entities
{
    public class Movie : AbstractEntity
    {
        
        public Movie()
        {
            
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public string MovieUrl { get; set; }
        public Genre Genre { get; set; }
        public List<Order> Orders { get; set; }
    }
}
