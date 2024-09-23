using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class Section625EDetailMap : EntityTypeConfiguration<Section625EDetail>
    {
        public Section625EDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Section625EDetailID);

            // Properties
            this.Property(t => t.Item)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Section625EDetail");
            this.Property(t => t.Section625EDetailID).HasColumnName("Section625EDetailID");
            this.Property(t => t.Section625EID).HasColumnName("Section625EID");
            this.Property(t => t.Item).HasColumnName("Item");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.ReplacementValue).HasColumnName("ReplacementValue");
            this.Property(t => t.Hour24Report625ID).HasColumnName("Hour24Report625ID");

            // Relationships
            this.HasRequired(t => t.Hour24Report625)
                .WithMany(t => t.Section625EDetail)
                .HasForeignKey(d => d.Hour24Report625ID);
            this.HasRequired(t => t.Section625E)
                .WithMany(t => t.Section625EDetail)
                .HasForeignKey(d => d.Section625EID);

        }
    }
}
