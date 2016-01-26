using System;
using System.Linq;
using System.Web.Mvc;
using AuctionHouseNew.Models;

namespace AuctionHouseNew.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var auctions = db.Auctions.ToList();

            foreach (var auc in auctions)
            {
                var timeLeft = auc.EndTime.Subtract(DateTime.Now);

                if (timeLeft.TotalSeconds <= 0.0)
                {
                    const string query = "SELECT * FROM Bids WHERE Auction_AuctionID = @p0";
                    var queryResult = db.Bids.SqlQuery(query, auc.AuctionID);

                    if (queryResult.Any())
                    {
                        var lastBidUser = queryResult.Last().BidderID;
                        auc.WinnerID = lastBidUser;
                        db.SaveChanges();
                    }
                }
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}