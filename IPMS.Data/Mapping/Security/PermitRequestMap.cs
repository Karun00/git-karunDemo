using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class PermitRequestMap : EntityTypeConfiguration<PermitRequest>
    {
        public PermitRequestMap()
        {
            // Primary Key
            this.HasKey(t => t.PermitRequestID);

            // Properties
            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.PermitRequestTypeCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.CompanyName)
                .HasMaxLength(50);

            this.Property(t => t.DepartmentName)
                .HasMaxLength(50);

            this.Property(t => t.ApplicantFullName)
                .HasMaxLength(200);

            this.Property(t => t.ApplicantSurName)
                .HasMaxLength(200);

            this.Property(t => t.PensionEmployeeNo)
                .HasMaxLength(20);

            this.Property(t => t.IDPassportNo)
                .HasMaxLength(50);

            this.Property(t => t.Occupation)
                .HasMaxLength(50);

            this.Property(t => t.HomeAddress)
                .HasMaxLength(500);

            this.Property(t => t.CompanyAddress)
                .HasMaxLength(500);

            this.Property(t => t.CompanyTelephoneNo)
                .HasMaxLength(20);

            this.Property(t => t.CompanyFaxNo)
                .HasMaxLength(20);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ReferenceNo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.permitStatus)
                .IsRequired()
                .HasMaxLength(4);
            this.Property(t => t.Email)
              .HasMaxLength(50);

            this.Property(t => t.MobileNo)
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("PermitRequest");
            this.Property(t => t.PermitRequestID).HasColumnName("PermitRequestID");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.PermitRequestTypeCode).HasColumnName("PermitRequestTypeCode");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.DepartmentName).HasColumnName("DepartmentName");
            this.Property(t => t.ApplicantFullName).HasColumnName("ApplicantFullName");
            this.Property(t => t.ApplicantSurName).HasColumnName("ApplicantSurName");
            this.Property(t => t.PensionEmployeeNo).HasColumnName("PensionEmployeeNo");
            this.Property(t => t.IDPassportNo).HasColumnName("IDPassportNo");
            this.Property(t => t.Occupation).HasColumnName("Occupation");
            this.Property(t => t.HomeAddress).HasColumnName("HomeAddress");
            this.Property(t => t.CompanyAddress).HasColumnName("CompanyAddress");
            this.Property(t => t.CompanyTelephoneNo).HasColumnName("CompanyTelephoneNo");
            this.Property(t => t.CompanyFaxNo).HasColumnName("CompanyFaxNo");
            this.Property(t => t.WorkflowInstanceId).HasColumnName("WorkflowInstanceId");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.ReferenceNo).HasColumnName("ReferenceNo");
            this.Property(t => t.permitStatus).HasColumnName("permitStatus");
            this.Property(t => t.PSOremarkes).HasColumnName("PSOremarkes");
            this.Property(t => t.AppealRemarks).HasColumnName("AppealRemarks");
            this.Property(t => t.AppealBoardRemarks).HasColumnName("AppealBoardRemarks");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.MobileNo).HasColumnName("MobileNo");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.PermitRequests)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.PermitRequests1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.PermitRequests)
                .HasForeignKey(d => d.PermitRequestTypeCode);
            this.HasRequired(t => t.SubCategory1)
                .WithMany(t => t.PermitRequests1)
                .HasForeignKey(d => d.permitStatus);
            this.HasRequired(t => t.Port)
                .WithMany(t => t.PermitRequests)
                .HasForeignKey(d => d.PortCode);
            this.HasOptional(t => t.WorkflowInstance)
                .WithMany(t => t.PermitRequests)
                .HasForeignKey(d => d.WorkflowInstanceId);

        }
    }
}
