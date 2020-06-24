using System.Data.Entity.ModelConfiguration;

namespace Gallery.DAL.Models.Configuration
{
    public class RoleConfig : EntityTypeConfiguration<Role>
    {
        public RoleConfig()
        {
            // Mapping a class to a table
            ToTable("Roles")
                //Set primary key
                .HasKey(p => p.Id);

            Property(p => p.Name)
                // Set column type
                .HasColumnType("varchar")
                // Set column size
                .HasMaxLength(16);
        }
    }
}