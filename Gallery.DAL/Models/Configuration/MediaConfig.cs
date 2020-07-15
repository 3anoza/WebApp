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
                .HasKey(m => m.Id);

            Property(m => m.Id)
                .IsRequired();

            Property(m => m.Path)
                .IsRequired()
                .HasMaxLength(500);

            HasIndex(m => m.Path)
                .IsUnique();

            Property(m => m.IsDeleted)
                .IsRequired();
        }
    }
}