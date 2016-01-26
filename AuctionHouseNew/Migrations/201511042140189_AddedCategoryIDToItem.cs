using System.Data.Entity.Migrations;

namespace AuctionHouseNew.Migrations
{
    public partial class AddedCategoryIDToItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "Category_CategoryID", "dbo.Categories");
            DropIndex("dbo.Items", new[] {"Category_CategoryID"});
            RenameColumn("dbo.Items", "Category_CategoryID", "CategoryID");
            AlterColumn("dbo.Items", "CategoryID", c => c.Int(false));
            CreateIndex("dbo.Items", "CategoryID");
            AddForeignKey("dbo.Items", "CategoryID", "dbo.Categories", "CategoryID", true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Items", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Items", new[] {"CategoryID"});
            AlterColumn("dbo.Items", "CategoryID", c => c.Int());
            RenameColumn("dbo.Items", "CategoryID", "Category_CategoryID");
            CreateIndex("dbo.Items", "Category_CategoryID");
            AddForeignKey("dbo.Items", "Category_CategoryID", "dbo.Categories", "CategoryID");
        }
    }
}