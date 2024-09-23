using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class ContractorPermitEmployeeDetailsMap: EntityTypeConfiguration<ContractorPermitEmployeeDetails>
    {
        public ContractorPermitEmployeeDetailsMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractorPermitEmployeeID);

            // Properties
            this.Property(t => t.EmployeeName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.IDNumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.JobTitle)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EmpSignature)
                .IsRequired()
                .HasMaxLength(50);

            

           

            // Table & Column Mappings
            this.ToTable("ContractorPermitEmployeeDetails");
            this.Property(t => t.ContractorPermitEmployeeID).HasColumnName("ContractorPermitEmployeeID");            
            this.Property(t => t.PermitRequestID).HasColumnName("PermitRequestID");
            this.Property(t => t.IDNumber).HasColumnName("IDNumber");
            this.Property(t => t.EmployeeName).HasColumnName("EmployeeName");
            this.Property(t => t.JobTitle).HasColumnName("JobTitle");
            this.Property(t => t.CriminalRecord).HasColumnName("CriminalRecord");
            this.Property(t => t.EmpSignature).HasColumnName("EmpSignature");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
           

            // Relationships
            this.HasRequired(t => t.PermitRequest)
                .WithMany(t => t.ContractorPermitEmployeeDetails)
                .HasForeignKey(d => d.PermitRequestID);
          

        }
    }
}

