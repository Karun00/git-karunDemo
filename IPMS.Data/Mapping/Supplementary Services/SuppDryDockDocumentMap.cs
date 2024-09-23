using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class SuppDryDockDocumentMap : EntityTypeConfiguration<SuppDryDockDocument>
    {
        public SuppDryDockDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.SuppDryDockDocumentID);

            // Properties
            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("SuppDryDockDocument");
            this.Property(t => t.SuppDryDockDocumentID).HasColumnName("SuppDryDockDocumentID");
            this.Property(t => t.SuppDryDockID).HasColumnName("SuppDryDockID");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Document)
                .WithMany(t => t.SuppDryDockDocuments)
                .HasForeignKey(d => d.DocumentID);
            this.HasRequired(t => t.SuppDryDock)
                .WithMany(t => t.SuppDryDockDocuments)
                .HasForeignKey(d => d.SuppDryDockID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.SuppDryDockDocuments)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.SuppDryDockDocuments1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
