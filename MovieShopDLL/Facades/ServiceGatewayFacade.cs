using ServiceGateway.Entities;
using ServiceGateway.ServiceGateways;

namespace ServiceGateway
{
    public class ServiceGatewayFacade
    {
        private IServiceGateway<Customer, int> cm;
        private IServiceGateway<Movie, int> mm;
        private IServiceGateway<Order, int> om;
        private IServiceGateway<Genre, int> gm;
        private IServiceGateway<Address, int> am;

        public IServiceGateway<Customer, int> GetCustomerServiceGateway()
        {
            return cm ?? (cm = new CustomerServiceGateway());
        }

        public IServiceGateway<Movie, int> GetMovieServiceGateway()
        {
            return mm ?? (mm = new MovieServiceGateway());
        }

        public IServiceGateway<Order, int> GetOrderServiceGateway()
        {
            return om ?? (om = new OrderServiceGateway());
        }

        public IServiceGateway<Genre, int> GetGenreServiceGateway()
        {
            return gm ?? (gm = new GenreServiceGateway());
        }

        public IServiceGateway<Address, int> GetAddressServiceGateway()
        {
            return am ?? (am = new AddressServiceGateway());
        }
    }
}
