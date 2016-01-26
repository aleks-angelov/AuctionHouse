using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AuctionHouseNew.Models;
using Microsoft.AspNet.Identity;

namespace AuctionHouseNew.Controllers
{
    public class BidsController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bids
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Bids.ToList());
        }

        // GET: Bids/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bid = db.Bids.Find(id);
            if (bid == null)
            {
                return HttpNotFound();
            }
            return View(bid);
        }

        // GET: Bids/Create
        [Authorize(Roles = "Customer")]
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }

        // POST: Bids/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BidID,Amount")] Bid bid, int id)
        {
            bid.BidderID = User.Identity.GetUserId().GetHashCode();
            bid.PlaceTime = DateTime.Now;
            const string query = "SELECT * FROM Auctions WHERE Item_ItemId = @p0";
            bid.Auction = db.Auctions.SqlQuery(query, id).Last();

            if (ModelState.IsValid && bid.Amount > bid.Auction.Item.Valuation)
            {
                bid.Auction.Item.Valuation = bid.Amount;
                bid.Auction.Bids.Add(bid);

                db.Bids.Add(bid);
                db.SaveChanges();
                return RedirectToAction("Details", "Items", new {id});
            }

            ModelState.AddModelError("", "The bid you place must be higher than the current bid.");
            return View(bid);
        }

        // GET: Bids/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bid = db.Bids.Find(id);
            if (bid == null)
            {
                return HttpNotFound();
            }
            return View(bid);
        }

        // POST: Bids/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BidID,BidderID,Amount,PlaceTime")] Bid bid)
        {
            if (ModelState.IsValid && bid.Amount > bid.Auction.Item.Valuation)
            {
                bid.Auction.Item.Valuation = bid.Amount;

                db.Entry(bid).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bid);
        }

        // GET: Bids/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bid = db.Bids.Find(id);
            if (bid == null)
            {
                return HttpNotFound();
            }
            return View(bid);
        }

        // POST: Bids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var bid = db.Bids.Find(id);
            db.Bids.Remove(bid);
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