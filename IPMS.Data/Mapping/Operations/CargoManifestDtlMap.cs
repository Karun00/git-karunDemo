using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class CargoManifestDtlMap : EntityTypeConfiguration<CargoManifestDtl>
    {
        public CargoManifestDtlMap()
        {
            // Primary Key
            this.HasKey(t => t.CargoManifestDtlID);


            // Properties
            this.Property(t => t.CargoTypeCode).IsRequired().HasMaxLength(4);
            this.Property(t => t.UOMCode).IsRequired().HasMaxLength(4);
            this.Property(t => t.RecordStatus).IsRequired().IsFixedLength().HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("CargoManifestDtl");
            this.Property(t => t.CargoManifestDtlID).HasColumnName("CargoManifestDtlID");
            this.Property(t => t.CargoManifestID).HasColumnName("CargoManifestID");
            this.Property(t => t.CargoTypeCode).HasColumnName("CargoTypeCode");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.UOMCode).HasColumnName("UOMCode");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.OutTurn).HasColumnName("OutTurn");
            
            // Relationships
            this.HasRequired(t => t.CargoManifest).WithMany(t => t.CargoManifestDtls).HasForeignKey(d => d.CargoManifestID);
            this.HasRequired(t => t.SubCategory).WithMany(t => t.CargoManifestDtl).HasForeignKey(d => d.CargoTypeCode);
            this.HasRequired(t => t.User).WithMany(t => t.CargoManifestDtl).HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1).WithMany(t => t.CargoManifestDtl1).HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SubCategory1).WithMany(t => t.CargoManifestDtl1).HasForeignKey(d => d.UOMCode);
        }
    }
}
