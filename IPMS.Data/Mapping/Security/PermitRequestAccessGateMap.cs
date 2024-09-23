using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class PermitRequestAccessGateMap : EntityTypeConfiguration<PermitRequestAccessGates>
    {
        public PermitRequestAccessGateMap()
        {
            // Primary Key
            this.HasKey(t => t.PermitRequestAccessGatesID);

            // Properties
            this.Property(t => t.AccessGates)
                .IsRequired()
                .HasMaxLength(4);

            // Table & Column Mappings
            this.ToTable("PermitRequestAccessGates");
            this.Property(t => t.PermitRequestAccessGatesID).HasColumnName("PermitRequestAccessGatesID");
            this.Property(t => t.WharfVehiclePermitID).HasColumnName("WharfVehiclePermitID");
            this.Property(t => t.AccessGates).HasColumnName("AccessGates");
            this.Property(t => t.PermitRequestID).HasColumnName("PermitRequestID");

            // Relationships
            this.HasRequired(t => t.PermitRequest)
                .WithMany(t => t.PermitRequestAccessGates)
                .HasForeignKey(d => d.PermitRequestID);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.PermitRequestAccessGates)
                .HasForeignKey(d => d.AccessGates);
            this.HasRequired(t => t.WharfVehiclePermit)
                .WithMany(t => t.PermitRequestAccessGates)
                .HasForeignKey(d => d.WharfVehiclePermitID);

        }
    }
}

