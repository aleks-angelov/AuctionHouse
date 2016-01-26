using System.Data.Entity.Migrations;

namespace AuctionHouseNew.Migrations
{
    public partial class AddedCategoryIDToRequest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ItemRequests", "Category_CategoryID", "dbo.Categories");
            DropIndex("dbo.ItemRequests", new[] {"Category_CategoryID"});
            RenameColumn("dbo.ItemRequests", "Category_CategoryID", "CategoryID");
            AlterColumn("dbo.ItemRequests", "CategoryID", c => c.Int(false));
            CreateIndex("dbo.ItemRequests", "CategoryID");
            AddForeignKey("dbo.ItemRequests", "CategoryID", "dbo.Categories", "CategoryID", true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.ItemRequests", "CategoryID", "dbo.Categories");
            DropIndex("dbo.ItemRequests", new[] {"CategoryID"});
            AlterColumn("dbo.ItemRequests", "CategoryID", c => c.Int());
            RenameColumn("dbo.ItemRequests", "CategoryID", "Category_CategoryID");
            CreateIndex("dbo.ItemRequests", "Category_CategoryID");
            AddForeignKey("dbo.ItemRequests", "Category_CategoryID", "dbo.Categories", "CategoryID");
        }
    }
}