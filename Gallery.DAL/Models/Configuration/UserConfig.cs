using System.Data.Entity.ModelConfiguration;

namespace Gallery.DAL.Models.Configuration
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            // Mapping a class to a table
            ToTable("Users")
                //Set primary key
                .HasKey(p => p.Id)
                // Not Null extensions
                .Property(p => p.Id).IsRequired();
            
            Property(p => p.Email)
                // Not Null extensions
                .IsRequired()
                // Set column type
                .HasColumnType("varchar")
                // Set column size
                .HasMaxLength(64);
            
            Property(p => p.Password)
                // Not Null extensions
                .IsRequired()
                // Set column type
                .HasColumnType("varchar")
                // Set column size
                .HasMaxLength(38);

            // Set relationship with other table
            HasMany(p => p.Roles)
                .WithMany(c => c.Users);

            // Set relationship with other table
            HasMany(p => p.Media)
                .WithRequired(c => c.User);

            // Set relationship with other table
            HasMany(p => p.Attempts)
                .WithRequired(c => c.User);

        }
    }
}