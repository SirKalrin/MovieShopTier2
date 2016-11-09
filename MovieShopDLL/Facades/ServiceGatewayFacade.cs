using ServiceGateway.Entities;
using ServiceGateway.ServiceGateways;

namespace ServiceGateway
{
    public class ServiceGatewayFacade
    {
        private AbstractServiceGateway<Customer, int> cm;
        private AbstractServiceGateway<Movie, int> mm;
        private AbstractServiceGateway<Order, int> om;
        private AbstractServiceGateway<Genre, int> gm;
        private AbstractServiceGateway<Address, int> am;

        public AbstractServiceGateway<Customer, int> GetCustomerServiceGateway()
        {
            return cm ?? (cm = new CustomerServiceGateway());
        }

        public AbstractServiceGateway<Movie, int> GetMovieServiceGateway()
        {
            return mm ?? (mm = new MovieServiceGateway());
        }

        public AbstractServiceGateway<Order, int> GetOrderServiceGateway()
        {
            return om ?? (om = new OrderServiceGateway());
        }

        public AbstractServiceGateway<Genre, int> GetGenreServiceGateway()
        {
            return gm ?? (gm = new GenreServiceGateway());
        }

        public AbstractServiceGateway<Address, int> GetAddressServiceGateway()
        {
            return am ?? (am = new AddressServiceGateway());
        }
    }
}
