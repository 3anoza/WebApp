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
        public DbSet<Attempts> Attempts { get; set; }

        // Fluent API method
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Mapping a class to a table
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Media>().ToTable("Media");
            modelBuilder.Entity<MediaType>().ToTable("MediaTypes");
            modelBuilder.Entity<Attempts>().ToTable("Attempts");

            // Set own primary key
            modelBuilder.Entity<User>().HasKey(p => p.Id);
            modelBuilder.Entity<Role>().HasKey(p => p.Id);
            modelBuilder.Entity<Media>().HasKey(p => p.Id);
            modelBuilder.Entity<MediaType>().HasKey(p => p.Id);
            modelBuilder.Entity<Attempts>().HasKey(p => p.Id);
            
            // Not Null extensions
            modelBuilder.Entity<User>().Property(p => p.Id).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.Email).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.Password).IsRequired();
            
            // Set column length limit
            modelBuilder.Entity<User>().Property(p => p.Email).HasMaxLength(64);
            modelBuilder.Entity<User>().Property(p => p.Password).HasMaxLength(38);
            modelBuilder.Entity<Role>().Property(p => p.Name).HasMaxLength(16);
            modelBuilder.Entity<Media>().Property(p => p.Path).HasMaxLength(25);
            modelBuilder.Entity<MediaType>().Property(p => p.Type).HasMaxLength(6);
            modelBuilder.Entity<Attempts>().Property(p => p.IpAddress).HasMaxLength(12);

            // Set column type
            modelBuilder.Entity<User>().Property(p => p.Email).HasColumnType("varchar");
            modelBuilder.Entity<User>().Property(p => p.Password).HasColumnType("varchar");
            modelBuilder.Entity<Role>().Property(p => p.Name).HasColumnType("varchar");
            modelBuilder.Entity<Media>().Property(p => p.Path).HasColumnType("varchar");
            modelBuilder.Entity<MediaType>().Property(p => p.Type).HasColumnType("varchar");
            modelBuilder.Entity<Attempts>().Property(p => p.IpAddress).HasColumnType("varchar");

            // Set relationships 
            // {
            
            modelBuilder.Entity<User>()
                .HasMany(p => p.Roles)
                .WithMany(c => c.Users);

            modelBuilder.Entity<User>()
                .HasMany(p => p.Media)
                .WithRequired(c => c.User);
            
            modelBuilder.Entity<MediaType>()
                .HasMany(p => p.Media)
                .WithRequired(c => c.MediaType);

            modelBuilder.Entity<User>()
                .HasMany(p => p.Attempts)
                .WithRequired(c => c.User);

            // }


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