using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor;
using ExchangeRate;
using MovieShopWepApp.Models;
using ServiceGateway;
using ServiceGateway.Entities;
using ServiceGateway.ServiceGateways;

namespace MovieShopWepApp.Controllers
{
    public class UserController : Controller
    {
        private IServiceGateway<Genre, int> _genreServiceGateway = new ServiceGatewayFacade().GetGenreServiceGateway();
        private IServiceGateway<Movie, int> _movieServiceGateway = new ServiceGatewayFacade().GetMovieServiceGateway();
        private IServiceGateway<Customer, int> _customerServiceGateway = new ServiceGatewayFacade().GetCustomerServiceGateway();
        private IServiceGateway<Order, int> _orderServiceGateway = new ServiceGatewayFacade().GetOrderServiceGateway();
        private Iso4217 _standard = Iso4217.DKK;

        // GET: User
        public ActionResult Index(int? id)
        {
            var model = new UserViewModel()
            {
                Genres = _genreServiceGateway.ReadAll(),
                Movies = _movieServiceGateway.ReadAll(),
                CurrentCurrency = _standard,
                GenreId = id
            };
            if (id.HasValue)
            {
                model.Genre = _genreServiceGateway.Read(id.Value);
            }
            return View(model);
        }

        public PartialViewResult GetMovieDetails(int? id, Iso4217 currency)
        {
            return PartialView("PartialMovieDetailsView",
                new PartialMovieDetailsViewModel()
                {
                    Movie = _movieServiceGateway.Read(id.Value),
                    CurrentCurrency = currency
                });
        }

        public PartialViewResult GetMoviesResult(int? id, Iso4217 currency)
        {
            if (id == null)
            {
                return PartialView("PartialMovieView",
                    new PartialViewModel()
                    {
                        Movies = _movieServiceGateway.ReadAll(),
                        CurrentCurrency = currency
                    });
            }
            var genre = _genreServiceGateway.Read(id.Value);
            foreach (var movie in genre.Movies)
            {
                movie.Genre = genre;
            }
            return PartialView("PartialMovieView", new PartialViewModel()
            {
                Movies = genre.Movies,
                CurrentCurrency = currency
            });
        }

        public ActionResult MovieDetails(int id)
        {
            return View(_movieServiceGateway.Read(id));
        }


        [HttpGet]
        public ActionResult Checkout(int id, string email)
        {
            var movieToOrder = _movieServiceGateway.Read(id);
            Customer c = SearchByEmail(email);

            var viewModel = new CheckoutViewModel() { customer = c, movie = movieToOrder };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout([Bind(Include = "Id,FirstName,LastName,Email, Address")] int id, Customer customer)
        {
            
            if (customer.Id < 1)
            {
                customer = _customerServiceGateway.Create(customer);
            }
            else
            {
                customer = _customerServiceGateway.Update(customer);
            }

            var order = new Order() { Movie = new Movie() { Id = id }, Customer = new Customer() { Id = customer.Id }, DateTime = DateTime.Now };

            //var movie = _movieServiceGateway.Read(id);
            //var order = new Order() { Movie = movie, Customer = customer, DateTime = DateTime.Now };
            _orderServiceGateway.Create(order);

            //movie.Orders.Add(order);
            //customer.Orders.Add(order);
            //_movieServiceGateway.Update(movie);
            //_customerServiceGateway.Update(customer);
            return RedirectToAction("Index");
        }

        public Customer SearchByEmail(string email)
        {
            if (email != null)
            {
                foreach (var customer in _customerServiceGateway.ReadAll())
                {
                    if (customer.Email != null)
                    {
                        if (customer.Email.Trim().Equals(email.Trim())) return customer;
                    }
                }
            }
            return null;
        }
    }
}