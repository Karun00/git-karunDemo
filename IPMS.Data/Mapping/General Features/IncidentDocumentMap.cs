using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class IncidentDocumentMap : EntityTypeConfiguration<IncidentDocument>
    {
        public IncidentDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.IncidentDocument1);

            // Properties
            this.Property(t => t.DocumentType)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.DocumentName)
                .HasMaxLength(100);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("IncidentDocument");
            this.Property(t => t.IncidentDocument1).HasColumnName("IncidentDocument");
            this.Property(t => t.IncidentID).HasColumnName("IncidentID");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.DocumentType).HasColumnName("DocumentType");
            this.Property(t => t.DocumentName).HasColumnName("DocumentName");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Document)
                .WithMany(t => t.IncidentDocuments)
                .HasForeignKey(d => d.DocumentID);
            this.HasRequired(t => t.Incident)
                .WithMany(t => t.IncidentDocuments)
                .HasForeignKey(d => d.IncidentID);

        }
    }
}
