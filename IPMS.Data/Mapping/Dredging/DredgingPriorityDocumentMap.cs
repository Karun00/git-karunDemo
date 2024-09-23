using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class DredgingPriorityDocumentMap : EntityTypeConfiguration<DredgingPriorityDocument>
    {
        public DredgingPriorityDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.DredgingPriorityDocumentID);

            // Properties
            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("DredgingPriorityDocument");
            this.Property(t => t.DredgingPriorityDocumentID).HasColumnName("DredgingPriorityDocumentID");
            this.Property(t => t.DredgingPriorityID).HasColumnName("DredgingPriorityID");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Document)
                .WithMany(t => t.DredgingPriorityDocuments)
                .HasForeignKey(d => d.DocumentID);
            this.HasRequired(t => t.DredgingPriority)
                .WithMany(t => t.DredgingPriorityDocuments)
                .HasForeignKey(d => d.DredgingPriorityID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.DredgingPriorityDocuments)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.DredgingPriorityDocuments1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
