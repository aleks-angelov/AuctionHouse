using System.Data.Entity.Migrations;

namespace AuctionHouseNew.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auctions",
                c => new
                {
                    AuctionID = c.Int(false, true),
                    StartTime = c.DateTime(false),
                    EndTime = c.DateTime(false),
                    WinnerID = c.Int(false),
                    Item_ItemID = c.Int()
                })
                .PrimaryKey(t => t.AuctionID)
                .ForeignKey("dbo.Items", t => t.Item_ItemID)
                .Index(t => t.Item_ItemID);

            CreateTable(
                "dbo.Bids",
                c => new
                {
                    BidID = c.Int(false, true),
                    BidderID = c.Int(false),
                    Amount = c.Decimal(false, 18, 2),
                    PlaceTime = c.DateTime(false),
                    Auction_AuctionID = c.Int()
                })
                .PrimaryKey(t => t.BidID)
                .ForeignKey("dbo.Auctions", t => t.Auction_AuctionID)
                .Index(t => t.Auction_AuctionID);

            CreateTable(
                "dbo.Items",
                c => new
                {
                    ItemID = c.Int(false, true),
                    Name = c.String(),
                    Description = c.String(),
                    Valuation = c.Decimal(false, 18, 2),
                    ImagePath = c.String(),
                    Category_CategoryID = c.Int()
                })
                .PrimaryKey(t => t.ItemID)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryID)
                .Index(t => t.Category_CategoryID);

            CreateTable(
                "dbo.Categories",
                c => new
                {
                    CategoryID = c.Int(false, true),
                    Name = c.String(),
                    ImagePath = c.String()
                })
                .PrimaryKey(t => t.CategoryID);

            CreateTable(
                "dbo.ItemRequests",
                c => new
                {
                    ItemRequestID = c.Int(false, true),
                    CustomerID = c.Int(false),
                    Name = c.String(),
                    Description = c.String(),
                    Valuation = c.Decimal(false, 18, 2),
                    ImagePath = c.String(),
                    Category_CategoryID = c.Int()
                })
                .PrimaryKey(t => t.ItemRequestID)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryID)
                .Index(t => t.Category_CategoryID);
        }

        public override void Down()
        {
            DropForeignKey("dbo.ItemRequests", "Category_CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Auctions", "Item_ItemID", "dbo.Items");
            DropForeignKey("dbo.Items", "Category_CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Bids", "Auction_AuctionID", "dbo.Auctions");
            DropIndex("dbo.ItemRequests", new[] {"Category_CategoryID"});
            DropIndex("dbo.Items", new[] {"Category_CategoryID"});
            DropIndex("dbo.Bids", new[] {"Auction_AuctionID"});
            DropIndex("dbo.Auctions", new[] {"Item_ItemID"});
            DropTable("dbo.ItemRequests");
            DropTable("dbo.Categories");
            DropTable("dbo.Items");
            DropTable("dbo.Bids");
            DropTable("dbo.Auctions");
        }
    }
}