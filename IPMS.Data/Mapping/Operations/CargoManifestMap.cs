using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class CargoManifestMap : EntityTypeConfiguration<CargoManifest>
    {
        public CargoManifestMap()
        {
            // Primary Key
            this.HasKey(t => t.CargoManifestID);

            // Properties
            this.Property(t => t.UOMCode).IsRequired().HasMaxLength(4);
            this.Property(t => t.VCN).IsRequired().HasMaxLength(12);
            this.Property(t => t.RecordStatus).IsRequired().IsFixedLength().HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("CargoManifest");
            this.Property(t => t.CargoManifestID).HasColumnName("CargoManifestID");
            this.Property(t => t.FirstMoveDateTime).HasColumnName("FirstMoveDateTime");
            this.Property(t => t.LastMoveDateTime).HasColumnName("LastMoveDateTime");
            this.Property(t => t.UOMCode).HasColumnName("UOMCode");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.VCN).HasColumnName("VCN");

            // Relationships
            this.HasRequired(t => t.ArrivalNotification).WithMany(t =>t.CargoManifests).HasForeignKey(d => d.VCN);
            this.HasRequired(t => t.User).WithMany(t => t.CargoManifest).HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1).WithMany(t => t.CargoManifest1).HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SubCategory).WithMany(t => t.CargoManifest).HasForeignKey(d => d.UOMCode);    
        }
    }
}
