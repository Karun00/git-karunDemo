using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class PermitRequestVerifyedDocumentMap : EntityTypeConfiguration<PermitRequestVerifyedDocument>
    {
        public PermitRequestVerifyedDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.PermitRequestverifyedDocumentID);

            // Properties
            this.Property(t => t.RecordStatus)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("PermitRequestVerifyedDocuments");
            this.Property(t => t.PermitRequestverifyedDocumentID).HasColumnName("PermitRequestverifyedDocumentID");
            this.Property(t => t.PermitRequestverifyedID).HasColumnName("PermitRequestverifyedID");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.DocumentName).HasColumnName("DocumentName");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");

            // Relationships
            this.HasOptional(t => t.Document)
                .WithMany(t => t.PermitRequestVerifyedDocuments)
                .HasForeignKey(d => d.DocumentID);
            this.HasOptional(t => t.PermitRequestVerifyedDetail)
                .WithMany(t => t.PermitRequestVerifyedDocuments)
                .HasForeignKey(d => d.PermitRequestverifyedID);

        }
    }
}
