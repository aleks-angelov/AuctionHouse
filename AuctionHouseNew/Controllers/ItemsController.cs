using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AuctionHouseNew.Models;

namespace AuctionHouseNew.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sold Items
        [Authorize(Roles = "Admin")]
        public ActionResult Sold()
        {
            const string query =
                "SELECT * FROM Items I WHERE EXISTS (SELECT * FROM Auctions A WHERE I.ItemID = A.Item_ItemID AND A.WinnerID > 0)";
            var queryResult = db.Items.SqlQuery(query);
            return View(queryResult.ToList());
        }

        // GET: Items
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.Category);
            return View(items.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            const string query = "SELECT * FROM Auctions WHERE Item_ItemID = @p0";
            var queryResult = db.Auctions.SqlQuery(query, id);
            ViewBag.HasAuction = queryResult.Any();

            return View(item);
        }

        // GET: Items/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemID,Name,Description,Valuation,ImagePath,CategoryID")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", item.CategoryID);
            return View(item);
        }

        // GET: Items/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", item.CategoryID);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemID,Name,Description,Valuation,ImagePath,CategoryID")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", item.CategoryID);
            return View(item);
        }

        // GET: Items/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var item = db.Items.Find(id);

            const string query = "SELECT * FROM Auctions WHERE Item_ItemID = @p0";
            var queryResult = db.Auctions.SqlQuery(query, item.ItemID);
            foreach (var auction in queryResult.ToList())
            {
                foreach (var bid in auction.Bids.ToList())
                    db.Bids.Remove(bid);

                db.Auctions.Remove(auction);
            }

            db.Items.Remove(item);
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