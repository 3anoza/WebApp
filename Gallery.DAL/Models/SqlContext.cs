using System.Data.Entity;

namespace Gallery.DAL.Models
{
    public class SqlContext: DbContext
    {
        public SqlContext()
        { }

        public SqlContext(string connectionString) : base(connectionString) 
        { }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new Configuration());
            base.OnModelCreating(modelBuilder);
        }
    }
}