namespace ServiceGateway.Entities
{
    public class Address : AbstractEntity
    {
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public int Zipcode { get; set; }
        public string City { get; set; }
        
    }
}
