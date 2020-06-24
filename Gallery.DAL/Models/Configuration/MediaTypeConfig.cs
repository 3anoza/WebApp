using System.Data.Entity.ModelConfiguration;

namespace Gallery.DAL.Models.Configuration
{
    public class MediaTypeConfig : EntityTypeConfiguration<MediaType>
    {
        public MediaTypeConfig()
        {
            // Mapping a class to a table
            ToTable("MediaTypes")
                //Set primary key
                .HasKey(p => p.Id);

            Property(p => p.Type)
                // Set column type
                .HasColumnType("varchar")
                // Set column size
                .HasMaxLength(6);
            
            // Set relationship with other table
            HasMany(p => p.Media)
                .WithRequired(c => c.MediaType);
        }
    }
}