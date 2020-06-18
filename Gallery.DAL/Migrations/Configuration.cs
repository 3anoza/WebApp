
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Gallery.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Gallery.DAL.Contexts.SqlContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    } 
}