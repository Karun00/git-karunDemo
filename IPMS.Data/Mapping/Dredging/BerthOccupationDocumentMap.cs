using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class BerthOccupationDocumentMap : EntityTypeConfiguration<BerthOccupationDocument>
    {
        public BerthOccupationDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.BerthOccupationDocumentID);

            // Properties
            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("BerthOccupationDocument");
            this.Property(t => t.BerthOccupationDocumentID).HasColumnName("BerthOccupationDocumentID");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.DredgingOperationID).HasColumnName("DredgingOperationID");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.BerthOccupationDocuments)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.Document)
                .WithMany(t => t.BerthOccupationDocuments)
                .HasForeignKey(d => d.DocumentID);
            this.HasRequired(t => t.DredgingOperation)
                .WithMany(t => t.BerthOccupationDocuments)
                .HasForeignKey(d => d.DredgingOperationID);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.BerthOccupationDocuments1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
