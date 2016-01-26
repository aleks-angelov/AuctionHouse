using System.Data.Entity.Migrations;

namespace AuctionHouseNew.Migrations
{
    public partial class RegistrationFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CreditCardNumber", c => c.String());
            AddColumn("dbo.AspNetUsers", "NameOnCard", c => c.String());
            AddColumn("dbo.AspNetUsers", "ExpirationDate", c => c.DateTime(false));
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            AddColumn("dbo.AspNetUsers", "City", c => c.String());
            AddColumn("dbo.AspNetUsers", "ZIPCode", c => c.Int(false));
            AddColumn("dbo.AspNetUsers", "Country", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Country");
            DropColumn("dbo.AspNetUsers", "ZIPCode");
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "FullName");
            DropColumn("dbo.AspNetUsers", "ExpirationDate");
            DropColumn("dbo.AspNetUsers", "NameOnCard");
            DropColumn("dbo.AspNetUsers", "CreditCardNumber");
        }
    }
}