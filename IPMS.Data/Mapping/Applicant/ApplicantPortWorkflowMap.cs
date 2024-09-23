using IPMS.Domain.Models;
using System.Data.Entity.ModelConfiguration;
namespace IPMS.Data.Mapping
{
    public class ApplicantPortWorkflowMap : EntityTypeConfiguration<ApplicantPortWorkflow>
    {
        public ApplicantPortWorkflowMap()
        {
            // Primary Key
            this.HasKey(t => t.ApplPortWorkflowID);

            // Properties
            this.Property(t => t.WFStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.RejectedRemarks)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Appl_Port_Workflow");
            this.Property(t => t.ApplPortWorkflowID).HasColumnName("ApplPortWorkflowID");
            this.Property(t => t.ApplicantID).HasColumnName("ApplicantID");
            this.Property(t => t.PortID).HasColumnName("PortID");
            this.Property(t => t.WFStatus).HasColumnName("WFStatus");
            this.Property(t => t.VerifiedBy).HasColumnName("VerifiedBy");
            this.Property(t => t.VerifiedDate).HasColumnName("VerifiedDate");
            this.Property(t => t.ApprovedBy).HasColumnName("ApprovedBy");
            this.Property(t => t.ApprovedDate).HasColumnName("ApprovedDate");
            this.Property(t => t.RejectedBy).HasColumnName("RejectedBy");
            this.Property(t => t.RejectedDate).HasColumnName("RejectedDate");
            this.Property(t => t.RejectedRemarks).HasColumnName("RejectedRemarks");
            this.Property(t => t.ReasonID).HasColumnName("ReasonID");

            // Relationships
            this.HasRequired(t => t.Applicant)
                .WithMany(t => t.Appl_Port_Workflow)
                .HasForeignKey(d => d.ApplicantID)
                .WillCascadeOnDelete(false);
            this.HasRequired(t => t.Port)
                .WithMany(t => t.Appl_Port_Workflow)
                .HasForeignKey(d => d.PortID)
                .WillCascadeOnDelete(false);
            this.HasOptional(t => t.Sub_Category)
                .WithMany(t => t.Appl_Port_Workflow)
                .HasForeignKey(d => d.ReasonID)
                .WillCascadeOnDelete(false);

        }
    }
}
