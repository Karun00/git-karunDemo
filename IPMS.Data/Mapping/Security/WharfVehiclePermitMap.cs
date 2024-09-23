using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class WharfVehiclePermitMap : EntityTypeConfiguration<WharfVehiclePermit>
    {
        public WharfVehiclePermitMap()
        {
            // Primary Key
            this.HasKey(t => t.WharfVehiclePermitID);

            // Properties
            this.Property(t => t.VehicleMake)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.VehicleModel)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.VehicleRegnNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.VehicleDescription)
                .HasMaxLength(200);

            this.Property(t => t.VehicleRegisterd)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.VehiclePointed)
                .IsFixedLength()
                .HasMaxLength(1);

            //this.Property(t => t.Reason)
            //    .HasMaxLength(200);

            this.Property(t => t.MobileNo)
                .HasMaxLength(15);

            this.Property(t => t.PermitRequirement)
                .HasMaxLength(4);

            this.Property(t => t.ContractorNo)
                .HasMaxLength(50);

            this.Property(t => t.TemporaryPermits)
                .HasMaxLength(4);

            this.Property(t => t.AccessGates)
                .HasMaxLength(4);

            this.Property(t => t.OtherSpecify)
                .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TelephoneNo)
                .HasMaxLength(15);

            this.Property(t => t.Hometelephone)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("WharfVehiclePermit");
            this.Property(t => t.WharfVehiclePermitID).HasColumnName("WharfVehiclePermitID");
            this.Property(t => t.PermitRequestID).HasColumnName("PermitRequestID");
            this.Property(t => t.VehicleMake).HasColumnName("VehicleMake");
            this.Property(t => t.VehicleModel).HasColumnName("VehicleModel");
            this.Property(t => t.VehicleRegnNo).HasColumnName("VehicleRegnNo");
            this.Property(t => t.VehicleDescription).HasColumnName("VehicleDescription");
            this.Property(t => t.VehicleRegisterd).HasColumnName("VehicleRegisterd");
            this.Property(t => t.VehiclePointed).HasColumnName("VehiclePointed");
            this.Property(t => t.Reason).HasColumnName("Reason");
            this.Property(t => t.MobileNo).HasColumnName("MobileNo");
            this.Property(t => t.PermitRequirement).HasColumnName("PermitRequirement");
            this.Property(t => t.ContractorNo).HasColumnName("ContractorNo");
            this.Property(t => t.TemporaryPermits).HasColumnName("TemporaryPermits");
            this.Property(t => t.AccessGates).HasColumnName("AccessGates");
            this.Property(t => t.OtherSpecify).HasColumnName("OtherSpecify");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.TelephoneNo).HasColumnName("TelephoneNo");
            this.Property(t => t.ContractDuration).HasColumnName("ContractDuration");
            this.Property(t => t.Hometelephone).HasColumnName("Hometelephone");

            // Relationships
            this.HasRequired(t => t.PermitRequest)
                .WithMany(t => t.WharfVehiclePermits)
                .HasForeignKey(d => d.PermitRequestID);
            this.HasOptional(t => t.SubCategory)
                .WithMany(t => t.WharfVehiclePermits)
                .HasForeignKey(d => d.AccessGates);
            this.HasOptional(t => t.SubCategory1)
                .WithMany(t => t.WharfVehiclePermits1)
                .HasForeignKey(d => d.PermitRequirement);
            this.HasOptional(t => t.SubCategory2)
                .WithMany(t => t.WharfVehiclePermits2)
                .HasForeignKey(d => d.TemporaryPermits);
            this.HasRequired(t => t.User)
                .WithMany(t => t.WharfVehiclePermits)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.WharfVehiclePermits1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
