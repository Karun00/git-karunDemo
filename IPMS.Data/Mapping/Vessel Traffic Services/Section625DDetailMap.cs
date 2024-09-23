using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class Section625DDetailMap : EntityTypeConfiguration<Section625DDetail>
    {
        public Section625DDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Section625DDetailID);

            // Properties
            this.Property(t => t.GroupCode)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.DetailCode)
                .IsRequired()
                .HasMaxLength(4);

            // Table & Column Mappings
            this.ToTable("Section625DDetail");
            this.Property(t => t.Section625DDetailID).HasColumnName("Section625DDetailID");
            this.Property(t => t.Section625DID).HasColumnName("Section625DID");
            this.Property(t => t.GroupCode).HasColumnName("GroupCode");
            this.Property(t => t.DetailCode).HasColumnName("DetailCode");
            this.Property(t => t.Hour24Report625ID).HasColumnName("Hour24Report625ID");

            // Relationships
            this.HasRequired(t => t.Hour24Report625)
                .WithMany(t => t.Section625DDetail)
                .HasForeignKey(d => d.Hour24Report625ID);
            this.HasRequired(t => t.Section625D)
                .WithMany(t => t.Section625DDetail)
                .HasForeignKey(d => d.Section625DID);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.Section625DDetail)
                .HasForeignKey(d => d.DetailCode);

        }
    }
}
