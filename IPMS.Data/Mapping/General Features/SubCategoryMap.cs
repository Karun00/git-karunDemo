using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class SubCategoryMap : EntityTypeConfiguration<SubCategory>
    {
        public SubCategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.SubCatCode);

            // Properties
            this.Property(t => t.SubCatCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.SupCatCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.SubCatName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.RecordStatus)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("SubCategory");
            this.Property(t => t.SubCatCode).HasColumnName("SubCatCode");
            this.Property(t => t.SupCatCode).HasColumnName("SupCatCode");
            this.Property(t => t.SubCatName).HasColumnName("SubCatName");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
        }
    }
}
