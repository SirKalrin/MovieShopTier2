using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using MovieShopWepApp.Models;
using ServiceGateway;
using ServiceGateway.Entities;

namespace MovieShopWepApp.Controllers
{
    //[RequireHttps] --> kan ikke bruges på Azure, certifikat er et krav.
    public class AdminController : Controller
    {
        private IServiceGateway<Customer, int> cusMgr = new ServiceGatewayFacade().GetCustomerServiceGateway();
        private IServiceGateway<Movie, int> movMgr = new ServiceGatewayFacade().GetMovieServiceGateway();
        private IServiceGateway<Order, int> ordMgr = new ServiceGatewayFacade().GetOrderServiceGateway();


        // GET: Admin
        public ActionResult Index()
        {
            try
            {
                return View(new AdminViewModel() { Customers = cusMgr.ReadAll(), Movies = movMgr.ReadAll(), Orders = ordMgr.ReadAll() });
            }
            catch (HttpRequestException ex)
            {
                if (ex.Message.Contains("401"))
                    return RedirectToAction("Login", "Account", new { returnUrl = Request.Url.LocalPath });

                ViewBag.Error = ex.Message;
                return View("Error");

            }
            
        }  


    }
}