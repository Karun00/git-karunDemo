using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class BerthVesselTypeMap : EntityTypeConfiguration<BerthVesselType>
    {
        public BerthVesselTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.BerthVesselTypeID);

            // Properties
            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.QuayCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.BerthCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.VesselTypeCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("BerthVesselType");
            this.Property(t => t.BerthVesselTypeID).HasColumnName("BerthVesselTypeID");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.QuayCode).HasColumnName("QuayCode");
            this.Property(t => t.BerthCode).HasColumnName("BerthCode");
            this.Property(t => t.VesselTypeCode).HasColumnName("VesselTypeCode");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Berth)
                .WithMany(t => t.BerthVesselTypes)
                .HasForeignKey(d => new { d.PortCode, d.QuayCode, d.BerthCode });
            this.HasRequired(t => t.User)
                .WithMany(t => t.BerthVesselTypes)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.BerthVesselTypes1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.BerthVesselTypes)
                .HasForeignKey(d => d.VesselTypeCode);

        }
    }
}
