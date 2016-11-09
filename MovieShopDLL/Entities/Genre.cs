using System.Collections.Generic;

namespace ServiceGateway.Entities
{
    public class Genre : AbstractEntity
    {
        public string Name { get; set; }
        //[JsonIgnore]
        //[IgnoreDataMember]
        public List<Movie> Movies { get; set; }
    }
}
