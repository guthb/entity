namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication1.DAL.BlogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApplication1.DAL.BlogContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Authors.AddOrUpdate(
                a => a.PenName,
                new Author { FirstName= "James", LastName = "Wilson", PenName = "SillyWilly"},
                new Author { FirstName = "Shut", LastName = "The", PenName = "FUCKUP" },
                new Author { FirstName = "Flap", LastName = "Flap", PenName = "HiGuys" }
            );

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


        }
    }
}
