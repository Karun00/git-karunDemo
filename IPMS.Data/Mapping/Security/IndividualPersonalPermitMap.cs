using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class IndividualPersonalPermitMap : EntityTypeConfiguration<IndividualPersonalPermit>
    {
        public IndividualPersonalPermitMap()
        {
            // Primary Key
            this.HasKey(t => t.IndividualPersonalPermitID);

            // Properties
            this.Property(t => t.permittype)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.IndividualTemporaryPermits)
                .IsFixedLength()
                .HasMaxLength(4);

            this.Property(t => t.IndividualPermanentPermits)
                .IsFixedLength()
                .HasMaxLength(4);

            this.Property(t => t.CameraDetails)
                .HasMaxLength(200);

            this.Property(t => t.ToolsDetails)
                .HasMaxLength(200);

            this.Property(t => t.SpclEquipmentDetails)
                .HasMaxLength(200);

        
            // Table & Column Mappings
            this.ToTable("IndividualPersonalPermit");
            this.Property(t => t.IndividualPersonalPermitID).HasColumnName("IndividualPersonalPermitID");
            this.Property(t => t.PermitRequestID).HasColumnName("PermitRequestID");
            this.Property(t => t.permittype).HasColumnName("permittype");
            this.Property(t => t.IndividualTemporaryPermits).HasColumnName("IndividualTemporaryPermits");
            this.Property(t => t.IndividualPermanentPermits).HasColumnName("IndividualPermanentPermits");
            this.Property(t => t.TempFromDate).HasColumnName("TempFromDate");
            this.Property(t => t.TempToDate).HasColumnName("TempToDate");
            this.Property(t => t.PerFromDate).HasColumnName("PerFromDate");
            this.Property(t => t.PerToDate).HasColumnName("PerToDate");
            this.Property(t => t.IsCamera).HasColumnName("IsCamera");
            this.Property(t => t.CameraDetails).HasColumnName("CameraDetails");
            this.Property(t => t.IsTools).HasColumnName("IsTools");
            this.Property(t => t.ToolsDetails).HasColumnName("ToolsDetails");
            this.Property(t => t.IsSpclEquipment).HasColumnName("IsSpclEquipment");
            this.Property(t => t.SpclEquipmentDetails).HasColumnName("SpclEquipmentDetails");
            this.Property(t => t.AuthorisedSurname).HasColumnName("AuthorisedSurname");
            this.Property(t => t.TelephoneWork).HasColumnName("TelephoneWork");
            this.Property(t => t.AuthorisedMobile).HasColumnName("AuthorisedMobile");
            this.Property(t => t.AuthorisedIdentityNumber).HasColumnName("AuthorisedIdentityNumber");
            this.Property(t => t.AuthorisedEmail).HasColumnName("AuthorisedEmail");
            this.Property(t => t.AuthorisedSignature).HasColumnName("AuthorisedSignature");
            this.Property(t => t.SignatoryDate).HasColumnName("SignatoryDate");

            this.Property(t => t.ContractorTemporaryPermits).HasColumnName("ContractorTemporaryPermits");
            this.Property(t => t.ContractorPermanentPermits).HasColumnName("ContractorPermanentPermits");
            this.Property(t => t.ContractorTempFromDate).HasColumnName("ContractorTempFromDate");
            this.Property(t => t.ContractorTempToDate).HasColumnName("ContractorTempToDate");
            this.Property(t => t.ContractorPerFromDate).HasColumnName("ContractorPerFromDate");
            this.Property(t => t.ContractorPerToDate).HasColumnName("ContractorPerToDate");
            




            // Relationships
            this.HasRequired(t => t.PermitRequest)
                .WithMany(t => t.IndividualPersonalPermits)
                .HasForeignKey(d => d.PermitRequestID);

            this.HasOptional(t => t.SubCategory)
                .WithMany(t => t.IndividualPersonalPermits)
                .HasForeignKey(d => d.IndividualTemporaryPermits);

            this.HasOptional(t => t.SubCategory1)
                .WithMany(t => t.IndividualPersonalPermits1)
                .HasForeignKey(d => d.IndividualPermanentPermits);

            this.HasOptional(t => t.SubCategory2)
             .WithMany(t => t.IndividualPersonalPermits2)
             .HasForeignKey(d => d.IndividualTemporaryPermits);

            this.HasOptional(t => t.SubCategory3)
                .WithMany(t => t.IndividualPersonalPermits3)
                .HasForeignKey(d => d.IndividualPermanentPermits);

           

        }
    }
}