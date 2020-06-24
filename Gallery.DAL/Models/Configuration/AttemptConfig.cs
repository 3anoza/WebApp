using System.Data.Entity.ModelConfiguration;

namespace Gallery.DAL.Models.Configuration
{
    public class AttemptConfig : EntityTypeConfiguration<Attempts>
    {
        public AttemptConfig()
        {
            // Mapping a class to a table
            ToTable("Attempts")
                //Set primary key
                .HasKey(p => p.Id);

            Property(p => p.TimeStamp)
                // Not Null extensions
                .IsRequired()
                // Set column type
                .HasColumnType("datetime2");
        }
    }
}