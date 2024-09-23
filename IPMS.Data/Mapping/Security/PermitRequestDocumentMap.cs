using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class PermitRequestDocumentMap : EntityTypeConfiguration<PermitRequestDocument>
    {
        public PermitRequestDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.PermitRequestDocumentID);

            // Properties
            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("PermitRequestDocument");
            this.Property(t => t.PermitRequestDocumentID).HasColumnName("PermitRequestDocumentID");
            this.Property(t => t.PermitRequestID).HasColumnName("PermitRequestID");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Document)
                .WithMany(t => t.PermitRequestDocuments)
                .HasForeignKey(d => d.DocumentID);
            this.HasRequired(t => t.PermitRequest)
                .WithMany(t => t.PermitRequestDocuments)
                .HasForeignKey(d => d.PermitRequestID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.PermitRequestDocuments)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.PermitRequestDocuments1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
