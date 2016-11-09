using System.Net;
using System.Web.Mvc;
using ServiceGateway;
using ServiceGateway.Entities;

namespace MovieShopWepApp.Controllers
{
    public class CustomersController : Controller
    {
        private IServiceGateway<Customer, int> CusMgr = new ServiceGatewayFacade().GetCustomerServiceGateway();
        private IServiceGateway<Order, int> OrdMgr = new ServiceGatewayFacade().GetOrderServiceGateway();
        private IServiceGateway<Movie, int> MovMgr = new ServiceGatewayFacade().GetMovieServiceGateway();
        private IServiceGateway<Address, int> AddMgr = new ServiceGatewayFacade().GetAddressServiceGateway();

        // GET: Customers
        public ActionResult Index()
        {
            return Redirect("~/Admin/Index");
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(AddMgr.ReadAll(), "Id", "StreetName");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                CusMgr.Create(customer);
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(AddMgr.ReadAll(), "Id", "StreetName", customer.Id);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = CusMgr.Read(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(AddMgr.ReadAll(), "Id", "StreetName", customer.Id);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                CusMgr.Update(customer);
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(AddMgr.ReadAll(), "Id", "StreetName", customer.Id);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = CusMgr.Read(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            foreach (var order in CusMgr.Read(id).Orders)
            {
                Order orderFromDatabase = OrdMgr.Read(order.Id);
                Movie movieToHaveOrdersRemoved = MovMgr.Read(orderFromDatabase.Movie.Id);
                movieToHaveOrdersRemoved.Orders.RemoveAll(x => x.Id == order.Id);
                MovMgr.Update(movieToHaveOrdersRemoved);
                OrdMgr.Delete(order.Id);
            }
            CusMgr.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
