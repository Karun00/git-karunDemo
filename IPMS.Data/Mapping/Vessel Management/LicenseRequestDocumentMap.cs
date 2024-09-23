using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class LicenseRequestDocumentMap : EntityTypeConfiguration<LicenseRequestDocument>
    {
        public LicenseRequestDocumentMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LicenseRequestID, t.DocumentID, t.RecordStatus, t.CreatedBy, t.CreatedDate, t.ModifiedBy, t.ModifiedDate });

            // Properties
            this.Property(t => t.LicenseRequestID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DocumentID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DocumentName)
                .HasMaxLength(100);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CreatedBy)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ModifiedBy)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("LicenseRequestDocument");
            this.Property(t => t.LicenseRequestID).HasColumnName("LicenseRequestID");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.DocumentName).HasColumnName("DocumentName");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Document)
                .WithMany(t => t.LicenseRequestDocuments)
                .HasForeignKey(d => d.DocumentID);
            this.HasRequired(t => t.LicenseRequest)
                .WithMany(t => t.LicenseRequestDocuments)
                .HasForeignKey(d => d.LicenseRequestID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.LicenseRequestDocuments)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.LicenseRequestDocuments1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
