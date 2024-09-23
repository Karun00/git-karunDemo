using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class VehiclePermitMap : EntityTypeConfiguration<VehiclePermit>
    {
        public VehiclePermitMap()
        {
            // Primary Key
            this.HasKey(t => t.VehiclePermitID);

            // Properties
            this.Property(t => t.VehicleMake)
                .HasMaxLength(50);

            this.Property(t => t.VehicleRegnNo)
                .HasMaxLength(50);

            this.Property(t => t.PermitRequirementCode)
                .HasMaxLength(4);

            //this.Property(t => t.Reason)
            //    .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("VehiclePermit");
            this.Property(t => t.VehiclePermitID).HasColumnName("VehiclePermitID");
            this.Property(t => t.PermitRequestID).HasColumnName("PermitRequestID");
            this.Property(t => t.VehicleMake).HasColumnName("VehicleMake");
            this.Property(t => t.VehicleRegnNo).HasColumnName("VehicleRegnNo");
            this.Property(t => t.PermitRequirementCode).HasColumnName("PermitRequirementCode");
            this.Property(t => t.Reason).HasColumnName("Reason");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.PermitRequest)
                .WithMany(t => t.VehiclePermits)
                .HasForeignKey(d => d.PermitRequestID);
            this.HasOptional(t => t.SubCategory)
                .WithMany(t => t.VehiclePermits)
                .HasForeignKey(d => d.PermitRequirementCode);
            this.HasRequired(t => t.User)
                .WithMany(t => t.VehiclePermits)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.VehiclePermits1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
