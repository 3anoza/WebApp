using System.Data.Entity.ModelConfiguration;

namespace Gallery.DAL.Models.Configuration
{
    public class MediaConfig : EntityTypeConfiguration<Media>
    {
        public MediaConfig()
        {
            // Mapping a class to a table
            ToTable("Media")
                //Set primary key
                .HasKey(p => p.Id);
            
            Property(p => p.Path)
                // Set column type
                .HasColumnType("varchar")
                // Set column size
                .HasMaxLength(25);
        }
    }
}