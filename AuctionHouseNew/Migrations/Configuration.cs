using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using AuctionHouseNew.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuctionHouseNew.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "AuctionHouseNew.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //Roles:
            context.Roles.AddOrUpdate(new IdentityRole
            {
                Id = "1",
                Name = "Admin"
            });

            context.Roles.AddOrUpdate(new IdentityRole
            {
                Id = "2",
                Name = "Customer"
            });

            context.SaveChanges();

            //Users:
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (!context.Users.Any(u => u.UserName == "jgalletly@aubg.edu"))
            {
                var adminToInsert = new ApplicationUser
                {
                    Id = "1",
                    Email = "jgalletly@aubg.edu",
                    UserName = "jgalletly@aubg.edu",
                    CreditCardNumber = "4111111111111111",
                    NameOnCard = "John Galletly",
                    ExpirationDate = new DateTime(2016, 1, 1),
                    FullName = "John Galletly",
                    Address = "259 MB",
                    City = "Blagoevgrad",
                    ZIPCode = 2700,
                    Country = "Bulgaria"
                };
                userManager.Create(adminToInsert, "Password1");
            }

            if (!context.Users.Any(u => u.UserName == "aia131@aubg.edu"))
            {
                var userToInsert = new ApplicationUser
                {
                    Id = "2",
                    Email = "aia131@aubg.edu",
                    UserName = "aia131@aubg.edu",
                    CreditCardNumber = "5555555555554444",
                    NameOnCard = "Aleks Angelov",
                    ExpirationDate = new DateTime(2017, 9, 9),
                    FullName = "Aleks Angelov",
                    Address = "1417 Skaptopara 1",
                    City = "Blagoevgrad",
                    ZIPCode = 2700,
                    Country = "Bulgaria"
                };
                userManager.Create(userToInsert, "Password1");
            }

            context.SaveChanges();

            userManager.AddToRole("1", "Admin");
            userManager.AddToRole("2", "Customer");

            context.SaveChanges();

            //Categories:
            var categories = new List<Category>
            {
                new Category
                {
                    CategoryID = 1,
                    Name = "Pottery",
                    ImagePath = "~/Content/Images/cat_pottery.png"
                },
                new Category
                {
                    CategoryID = 2,
                    Name = "Art",
                    ImagePath = "~/Content/Images/cat_art.png"
                },
                new Category
                {
                    CategoryID = 3,
                    Name = "Stamps",
                    ImagePath = "~/Content/Images/cat_stamps.png"
                },
                new Category
                {
                    CategoryID = 4,
                    Name = "Coins",
                    ImagePath = "~/Content/Images/cat_coins.png"
                },
                new Category
                {
                    CategoryID = 5,
                    Name = "Bric-a-brac",
                    ImagePath = "~/Content/Images/cat_bricabrac.png"
                }
            };

            categories.ForEach(c => context.Categories.AddOrUpdate(c));

            context.SaveChanges();

            //Items:
            var items = new List<Item>
            {
                new Item
                {
                    ItemID = 1,
                    Name = "Vase 1",
                    Description = "Archaic vase",
                    Valuation = 100,
                    ImagePath = "~/Content/Images/item_pottery_1.jpg",
                    Category = categories.Find(c => c.Name == "Pottery")
                },
                new Item
                {
                    ItemID = 2,
                    Name = "Painting 1",
                    Description = "Claude Monet painting",
                    Valuation = 120,
                    ImagePath = "~/Content/Images/item_art_1.jpg",
                    Category = categories.Find(c => c.Name == "Art")
                },
                new Item
                {
                    ItemID = 3,
                    Name = "Painting 2",
                    Description = "Monet painting",
                    Valuation = 110,
                    ImagePath = "~/Content/Images/item_art_2.jpg",
                    Category = categories.Find(c => c.Name == "Art")
                },
                new Item
                {
                    ItemID = 4,
                    Name = "Coin",
                    Description = "Ancient coin",
                    Valuation = 90,
                    ImagePath = "~/Content/Images/item_coin_1.jpg",
                    Category = categories.Find(c => c.Name == "Coins")
                },
                new Item
                {
                    ItemID = 5,
                    Name = "Monet in Bric-a-brac",
                    Description = "Monet painting in Misc.",
                    Valuation = 80,
                    ImagePath = "~/Content/Images/item_bricabrac_1.jpg",
                    Category = categories.Find(c => c.Name == "Bric-a-brac")
                }
            };

            items.ForEach(i => context.Items.AddOrUpdate(i));

            context.SaveChanges();

            //Item requests:

            //Auctions:

            //Bids:
        }
    }
}