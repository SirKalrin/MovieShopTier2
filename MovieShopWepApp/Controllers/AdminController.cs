using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieShopWepApp.Models;
using ServiceGateway;
using ServiceGateway.Entities;

namespace MovieShopWepApp.Controllers
{
    [Authorize]
    //[RequireHttps] --> kan ikke bruges på Azure, certifikat er et krav.
    public class AdminController : Controller
    {
        private IServiceGateway<Customer, int> cusMgr = new ServiceGatewayFacade().GetCustomerServiceGateway();
        private IServiceGateway<Movie, int> movMgr = new ServiceGatewayFacade().GetMovieServiceGateway();
        private IServiceGateway<Order, int> ordMgr = new ServiceGatewayFacade().GetOrderServiceGateway();

        [AllowAnonymous]
        // GET: Admin
        public ActionResult Index()
        {
            
            return View(new AdminViewModel() {Customers = cusMgr.ReadAll(), Movies = movMgr.ReadAll(), Orders = ordMgr.ReadAll()});
        }  


    }
}