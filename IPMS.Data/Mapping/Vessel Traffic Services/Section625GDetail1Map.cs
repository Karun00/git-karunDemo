using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class Section625GDetail1Map : EntityTypeConfiguration<Section625GDetail1>
    {
        public Section625GDetail1Map()
        {
            // Primary Key
            this.HasKey(t => t.Section625GDetail1ID);

            // Properties
            this.Property(t => t.RISubCatCode)
                .IsRequired()
                .HasMaxLength(4);

            // Table & Column Mappings
            this.ToTable("Section625GDetail1");
            this.Property(t => t.Section625GDetail1ID).HasColumnName("Section625GDetail1ID");
            this.Property(t => t.Section625GID).HasColumnName("Section625GID");
            this.Property(t => t.RISubCatCode).HasColumnName("RISubCatCode");
            this.Property(t => t.Hour24Report625ID).HasColumnName("Hour24Report625ID");

            // Relationships
            this.HasRequired(t => t.Hour24Report625)
                .WithMany(t => t.Section625GDetail1)
                .HasForeignKey(d => d.Hour24Report625ID);
            this.HasRequired(t => t.Section625G)
                .WithMany(t => t.Section625GDetail1)
                .HasForeignKey(d => d.Section625GID);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.Section625GDetail1)
                .HasForeignKey(d => d.RISubCatCode);

        }
    }
}
