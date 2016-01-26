using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AuctionHouseNew.Models;
using Microsoft.AspNet.Identity;

namespace AuctionHouseNew.Controllers
{
    public class ItemRequestsController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: ItemRequests/Аpprove/5
        [Authorize(Roles = "Admin")]
        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var itemRequest = db.ItemRequests.Find(id);
            if (itemRequest == null)
            {
                return HttpNotFound();
            }
            return View(itemRequest);
        }

        // POST: ItemRequests/Аpprove/5
        [HttpPost, ActionName("Approve")]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveConfirmed(int id)
        {
            var itemRequest = db.ItemRequests.Find(id);

            var item = new Item
            {
                Name = itemRequest.Name,
                Description = itemRequest.Description,
                Valuation = itemRequest.Valuation,
                ImagePath = itemRequest.ImagePath,
                CategoryID = itemRequest.CategoryID
            };

            db.Items.Add(item);
            db.ItemRequests.Remove(itemRequest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: ItemRequests
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var itemRequests = db.ItemRequests.Include(i => i.Category);
            return View(itemRequests.ToList());
        }

        // GET: ItemRequests/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var itemRequest = db.ItemRequests.Find(id);
            if (itemRequest == null)
            {
                return HttpNotFound();
            }
            return View(itemRequest);
        }

        // GET: ItemRequests/Create
        [Authorize(Roles = "Customer")]
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            return View();
        }

        // POST: ItemRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "ItemRequestID,CustomerID,Name,Description,Valuation,ImagePath,CategoryID")] ItemRequest
                itemRequest)
        {
            itemRequest.CustomerID = User.Identity.GetUserId().GetHashCode();

            if (ModelState.IsValid)
            {
                db.ItemRequests.Add(itemRequest);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", itemRequest.CategoryID);
            return View(itemRequest);
        }

        // GET: ItemRequests/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var itemRequest = db.ItemRequests.Find(id);
            if (itemRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", itemRequest.CategoryID);
            return View(itemRequest);
        }

        // POST: ItemRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "ItemRequestID,CustomerID,Name,Description,Valuation,ImagePath,CategoryID")] ItemRequest
                itemRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", itemRequest.CategoryID);
            return View(itemRequest);
        }

        // GET: ItemRequests/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var itemRequest = db.ItemRequests.Find(id);
            if (itemRequest == null)
            {
                return HttpNotFound();
            }
            return View(itemRequest);
        }

        // POST: ItemRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var itemRequest = db.ItemRequests.Find(id);
            db.ItemRequests.Remove(itemRequest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}