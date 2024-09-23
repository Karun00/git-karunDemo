using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class ContractorPermitApplicationDetailsMap: EntityTypeConfiguration<ContractorPermitApplicationDetails>
    {
        public ContractorPermitApplicationDetailsMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractorPermitApplicationID);

            // Properties
            this.Property(t => t.ContractorCompanyName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ContractorCompanyManager)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Department)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TelephoneNumber)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.SubContractorCompanyName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SubContractorTelephoneNumber)
                .IsRequired()
                .HasMaxLength(15);

           

            // Table & Column Mappings
            this.ToTable("ContractorPermitApplicationDetails");
            this.Property(t => t.ContractorPermitApplicationID).HasColumnName("ContractorPermitApplicationID");
            this.Property(t => t.PermitRequestID).HasColumnName("PermitRequestID");
            this.Property(t => t.ContractorCompanyName).HasColumnName("ContractorCompanyName");
            this.Property(t => t.ContractorCompanyManager).HasColumnName("ContractorCompanyManager");
            this.Property(t => t.Department).HasColumnName("Department");
            this.Property(t => t.TelephoneNumber).HasColumnName("TelephoneNumber");
            this.Property(t => t.SubContractorCompanyName).HasColumnName("SubContractorCompanyName");
            this.Property(t => t.SubContractorTelephoneNumber).HasColumnName("SubContractorTelephoneNumber");            
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.PermitRequest)
                .WithMany(t => t.ContractorPermitApplicationDetails)
                .HasForeignKey(d => d.PermitRequestID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.ContractorPermitApplicationDetails)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.ContractorPermitApplicationDetails1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}

