using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MovieShopWepApp.Models;
using ServiceGateway.ServiceGateways;

namespace MovieShopWepApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        AuthorizationServiceGateway gateway = new AuthorizationServiceGateway();



        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = gateway.Login(model.UserName, model.Password);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt!");
                }
            }
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = gateway.Register(model);

                if (response.IsSuccessStatusCode)
                {
                    gateway.Login(model.Email, model.Password);
                    return RedirectToAction("Login", "Account");
                }
                else
                    ModelState.AddModelError("", response.Content.ReadAsStringAsync().Result);
            }

            return View(model);
        }


      
    }
}