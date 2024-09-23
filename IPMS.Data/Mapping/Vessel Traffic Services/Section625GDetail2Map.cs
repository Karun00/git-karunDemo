using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class Section625GDetail2Map : EntityTypeConfiguration<Section625GDetail2>
    {
        public Section625GDetail2Map()
        {
            // Primary Key
            this.HasKey(t => t.Section625GDetail2ID);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(200);

            this.Property(t => t.ResponsiblePerson)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Section625GDetail2");
            this.Property(t => t.Section625GDetail2ID).HasColumnName("Section625GDetail2ID");
            this.Property(t => t.Section625GID).HasColumnName("Section625GID");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.ResponsiblePerson).HasColumnName("ResponsiblePerson");
            this.Property(t => t.TargetCompletion).HasColumnName("TargetCompletion");
            this.Property(t => t.DateCompletion).HasColumnName("DateCompletion");
            this.Property(t => t.Hour24Report625ID).HasColumnName("Hour24Report625ID");

            // Relationships
            this.HasRequired(t => t.Hour24Report625)
                .WithMany(t => t.Section625GDetail2)
                .HasForeignKey(d => d.Hour24Report625ID);
            this.HasRequired(t => t.Section625G)
                .WithMany(t => t.Section625GDetail2)
                .HasForeignKey(d => d.Section625GID);

        }
    }
}
