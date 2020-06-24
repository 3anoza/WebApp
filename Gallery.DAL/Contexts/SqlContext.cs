using System.Data.Entity;
using Gallery.DAL.Models;
using Gallery.DAL.Models.Configuration;

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
        public DbSet<Attempts> Attempts { get; set; }

        // Fluent API method
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AttemptConfig());
            modelBuilder.Configurations.Add(new MediaConfig());
            modelBuilder.Configurations.Add(new MediaTypeConfig());
            modelBuilder.Configurations.Add(new RoleConfig());
            modelBuilder.Configurations.Add(new UserConfig());
            
            base.OnModelCreating(modelBuilder);
        }
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