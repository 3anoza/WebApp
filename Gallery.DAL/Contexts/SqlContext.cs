using System.Data.Entity;
using Gallery.DAL.Models;

namespace Gallery.DAL.Contexts
{
    public class SqlContext: DbContext
    {
        public SqlContext()
        { }

        public SqlContext(string connectionString) : base(connectionString) 
        { }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new Configuration());
            base.OnModelCreating(modelBuilder);
        }*/
    }

    public class UserDbInitializer : DropCreateDatabaseAlways<SqlContext>
    {
        protected override void Seed(SqlContext context)
        {
            context.Roles.Add(new Role { Id = 1, Name = "admin"});
            context.Roles.Add(new Role { Id = 2, Name = "user"});
            base.Seed(context);
        }
    }
}