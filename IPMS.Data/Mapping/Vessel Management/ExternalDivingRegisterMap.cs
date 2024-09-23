using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class ExternalDivingRegisterMap : EntityTypeConfiguration<ExternalDivingRegister>
    {
        public ExternalDivingRegisterMap()
        {
            // Primary Key
            this.HasKey(t => t.ExternalDivingRegisterID);

            // Properties            
            this.Property(t => t.PersonInCharge)
                .HasMaxLength(200);

            this.Property(t => t.PortCode)
              .IsRequired()
              .HasMaxLength(2);

            this.Property(t => t.QuayCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.BerthCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.OnsiteSupervisorContNo)
                .HasMaxLength(20);

            this.Property(t => t.OffsiteSupervisorContNo)
                .HasMaxLength(20);

            this.Property(t => t.ClearanceNo)
                .HasMaxLength(200);

            this.Property(t => t.PermissionObtained)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("ExternalDivingRegister");
            this.Property(t => t.ExternalDivingRegisterID).HasColumnName("ExternalDivingRegisterID");
            this.Property(t => t.DivingLogDateTime).HasColumnName("DivingLogDateTime");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");            
            this.Property(t => t.VesselID).HasColumnName("VesselID");
            this.Property(t => t.PersonInCharge).HasColumnName("PersonInCharge");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.StopTime).HasColumnName("StopTime");
            this.Property(t => t.OnsiteSupervisorContNo).HasColumnName("OnsiteSupervisorContNo");
            this.Property(t => t.OffsiteSupervisorContNo).HasColumnName("OffsiteSupervisorContNo");
            this.Property(t => t.ClearanceNo).HasColumnName("ClearanceNo");
            this.Property(t => t.NoOfDrivers).HasColumnName("NoOfDrivers");
            this.Property(t => t.PermissionObtained).HasColumnName("PermissionObtained");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.QuayCode).HasColumnName("QuayCode");
            this.Property(t => t.BerthCode).HasColumnName("BerthCode");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.ExternalDivingRegisters)
                .HasForeignKey(d => d.CreatedBy);
            
            this.HasRequired(t => t.User1)
                .WithMany(t => t.ExternalDivingRegisters1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasOptional(t => t.Vessel)
                .WithMany(t => t.ExternalDivingRegisters)
                .HasForeignKey(d => d.VesselID);
            this.HasRequired(t => t.Berth)
                .WithMany(t => t.ExternalDivingRegisters)
                .HasForeignKey(d => new { d.PortCode, d.QuayCode, d.BerthCode });

        }
    }
}
