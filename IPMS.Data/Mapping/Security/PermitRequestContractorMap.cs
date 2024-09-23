using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class PermitRequestContractorMap : EntityTypeConfiguration<PermitRequestContractor>
    {
        public PermitRequestContractorMap()
        {
            // Primary Key
            this.HasKey(t => t.PermitRequestContractorID);

            // Properties
            this.Property(t => t.CompanyName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ContractNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ContractManagerName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ServiceCompanyName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ResponsibleManager)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ContactNo)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.MobileNo)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("PermitRequestContractor");
            this.Property(t => t.PermitRequestContractorID).HasColumnName("PermitRequestContractorID");
            this.Property(t => t.PermitRequestID).HasColumnName("PermitRequestID");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.ContractNo).HasColumnName("ContractNo");
            this.Property(t => t.ContractManagerName).HasColumnName("ContractManagerName");
            this.Property(t => t.ContractDuration).HasColumnName("ContractDuration");
            this.Property(t => t.ServiceCompanyName).HasColumnName("ServiceCompanyName");
            this.Property(t => t.ResponsibleManager).HasColumnName("ResponsibleManager");
            this.Property(t => t.ContactNo).HasColumnName("ContactNo");
            this.Property(t => t.MobileNo).HasColumnName("MobileNo");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.PermitRequest)
                .WithMany(t => t.PermitRequestContractors)
                .HasForeignKey(d => d.PermitRequestID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.PermitRequestContractors)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.PermitRequestContractors1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
