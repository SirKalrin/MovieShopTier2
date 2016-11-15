﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor;
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

        // GET: User
        public ActionResult Index(int? id)
        {
            var model = new UserViewModel()
            {
                Genres = _genreServiceGateway.ReadAll(),
                Movies = _movieServiceGateway.ReadAll()
            };
            if (id.HasValue)
            {
                model.Genre = _genreServiceGateway.Read(id.Value);
            }
            return View(model);
        }

        public PartialViewResult GetMovieDetails(int? id)
        {
            return PartialView("PartialMovieDetailsView", _movieServiceGateway.Read(id.Value));
        }

        public PartialViewResult GetMoviesResult(int? id)
        {
            var genre = _genreServiceGateway.Read(id.Value);
            foreach (var movie in genre.Movies)
            {
                movie.Genre = genre;
            }
            return PartialView("PartialMovieView", genre.Movies);
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

            var order = new Order() {Movie = new Movie() {Id = id}, Customer = new Customer() { Id = customer.Id }, DateTime = DateTime.Now};

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