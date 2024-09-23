using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class Section625CDetailMap : EntityTypeConfiguration<Section625CDetail>
    {
        public Section625CDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Section625CDetailID);

            // Properties
            this.Property(t => t.GroupCode)
                .IsRequired()
                .HasMaxLength(6);

            this.Property(t => t.DetailCode)
                .IsRequired()
                .HasMaxLength(4);

            // Table & Column Mappings
            this.ToTable("Section625CDetail");
            this.Property(t => t.Section625CDetailID).HasColumnName("Section625CDetailID");
            this.Property(t => t.Section625CID).HasColumnName("Section625CID");
            this.Property(t => t.GroupCode).HasColumnName("GroupCode");
            this.Property(t => t.DetailCode).HasColumnName("DetailCode");
            this.Property(t => t.Hour24Report625ID).HasColumnName("Hour24Report625ID");

            // Relationships
            this.HasRequired(t => t.Hour24Report625)
                .WithMany(t => t.Section625CDetail)
                .HasForeignKey(d => d.Hour24Report625ID);
            this.HasRequired(t => t.Section625C)
                .WithMany(t => t.Section625CDetail)
                .HasForeignKey(d => d.Section625CID);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.Section625CDetail)
                .HasForeignKey(d => d.DetailCode);

        }
    }
}
