using System.Net;
using System.Web.Mvc;
using ServiceGateway;
using ServiceGateway.Entities;
using ServiceGateway.ServiceGateways;

namespace MovieShopWepApp.Controllers
{
    public class MoviesController : Controller
    {
        private AbstractServiceGateway<Movie, int> MovMgr = new ServiceGatewayFacade().GetMovieServiceGateway();
        private AbstractServiceGateway<Order, int> OrdMgr = new ServiceGatewayFacade().GetOrderServiceGateway();
        private AbstractServiceGateway<Customer, int> CusMgr = new ServiceGatewayFacade().GetCustomerServiceGateway();
        private AbstractServiceGateway<Genre, int> GenMgr = new ServiceGatewayFacade().GetGenreServiceGateway();

        // GET: Movies
        public ActionResult Index()
        {
            return Redirect("~/Admin/Index");
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = MovMgr.Read(id.Value);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(GenMgr.ReadAll(), "Id", "Name");
            ViewBag.Id = new SelectList(OrdMgr.ReadAll(), "Id", "Id");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Year,Price,ImageUrl,MovieUrl,Genre")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                MovMgr.Create(movie);
                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(GenMgr.ReadAll(), "Id", "Name", movie.Genre.Id);
            ViewBag.Id = new SelectList(OrdMgr.ReadAll(), "Id", "Id", movie.Id);
            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = MovMgr.Read(id.Value);
            if (movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreId = new SelectList(GenMgr.ReadAll(), "Id", "Name", movie.Genre.Id);
            ViewBag.Id = new SelectList(OrdMgr.ReadAll(), "Id", "Id", movie.Id);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Year,Price,ImageUrl,MovieUrl,GenreId")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                MovMgr.Update(movie);
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(GenMgr.ReadAll(), "Id", "Name", movie.Genre.Id);
            ViewBag.Id = new SelectList(OrdMgr.ReadAll(), "Id", "Id", movie.Id);
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = MovMgr.Read(id.Value);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            foreach (Order ord in OrdMgr.ReadAll())
            {
                if (ord.Movie.Id == id)
                {
                    Customer customerToHaveOrderRemoved = CusMgr.Read(ord.Customer.Id);
                    customerToHaveOrderRemoved.Orders.RemoveAll(x => x.Id == ord.Id);
                    CusMgr.Update(customerToHaveOrderRemoved);
                    OrdMgr.Delete(ord.Id);
                }
            }
            MovMgr.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
