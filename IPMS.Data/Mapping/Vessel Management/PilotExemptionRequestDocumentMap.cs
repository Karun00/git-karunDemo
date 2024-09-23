using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class PilotExemptionRequestDocumentMap : EntityTypeConfiguration<PilotExemptionRequestDocument>
    {
        public PilotExemptionRequestDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.PilotExemptionRequestDocumentID);

            // Properties
            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.FileName)
                .HasMaxLength(200);

            this.Property(t => t.DocumentName)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("PilotExemptionRequestDocument");
            this.Property(t => t.PilotExemptionRequestDocumentID).HasColumnName("PilotExemptionRequestDocumentID");
            this.Property(t => t.PilotID).HasColumnName("PilotID");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.DocumentName).HasColumnName("DocumentName");

            // Relationships
            this.HasRequired(t => t.Document)
                .WithMany(t => t.PilotExemptionRequestDocuments)
                .HasForeignKey(d => d.DocumentID);
            this.HasRequired(t => t.Pilot)
                .WithMany(t => t.PilotExemptionRequestDocuments)
                .HasForeignKey(d => d.PilotID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.PilotExemptionRequestDocuments)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.PilotExemptionRequestDocuments1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
