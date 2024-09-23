using IPMS.Domain.Models;
using System.Data.Entity.ModelConfiguration;
namespace IPMS.Data.Mapping
{
    public class ApplicantAttachmentMap : EntityTypeConfiguration<ApplicantAttachment>
    {
        public ApplicantAttachmentMap()
        {
            // Primary Key
            this.HasKey(t => t.ApplAttachID);

            // Properties
            this.Property(t => t.DocuPath)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Status)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Appl_Attachment");
            this.Property(t => t.ApplAttachID).HasColumnName("ApplAttachID");
            this.Property(t => t.ApplicantID).HasColumnName("ApplicantID");
            this.Property(t => t.DocuType).HasColumnName("DocuType");
            this.Property(t => t.DocuPath).HasColumnName("DocuPath");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Applicant)
                .WithMany(t => t.Appl_Attachments)
                .HasForeignKey(d => d.ApplicantID);
            this.HasRequired(t => t.Sub_Category)
                .WithMany(t => t.Appl_Attachments)
                .HasForeignKey(d => d.DocuType);

        }
    }
}
