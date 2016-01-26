using System.Data.Entity.Migrations;

namespace AuctionHouseNew.Migrations
{
    public partial class RequiredFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Items", "Name", c => c.String(false));
            AlterColumn("dbo.Items", "Description", c => c.String(false));
            AlterColumn("dbo.Items", "ImagePath", c => c.String(false));
            AlterColumn("dbo.Categories", "Name", c => c.String(false));
            AlterColumn("dbo.Categories", "ImagePath", c => c.String(false));
            AlterColumn("dbo.ItemRequests", "Name", c => c.String(false));
            AlterColumn("dbo.ItemRequests", "Description", c => c.String(false));
            AlterColumn("dbo.ItemRequests", "ImagePath", c => c.String(false));
        }

        public override void Down()
        {
            AlterColumn("dbo.ItemRequests", "ImagePath", c => c.String());
            AlterColumn("dbo.ItemRequests", "Description", c => c.String());
            AlterColumn("dbo.ItemRequests", "Name", c => c.String());
            AlterColumn("dbo.Categories", "ImagePath", c => c.String());
            AlterColumn("dbo.Categories", "Name", c => c.String());
            AlterColumn("dbo.Items", "ImagePath", c => c.String());
            AlterColumn("dbo.Items", "Description", c => c.String());
            AlterColumn("dbo.Items", "Name", c => c.String());
        }
    }
}