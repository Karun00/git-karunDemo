using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class SuppHotColdWorkPermitDocumentMap : EntityTypeConfiguration<SuppHotColdWorkPermitDocument>
    {
        public SuppHotColdWorkPermitDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.SuppHotColdWorkPermitDocumentID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SuppHotColdWorkPermitDocument");
            this.Property(t => t.SuppHotColdWorkPermitDocumentID).HasColumnName("SuppHotColdWorkPermitDocumentID");
            this.Property(t => t.SuppHotColdWorkPermitID).HasColumnName("SuppHotColdWorkPermitID");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Document)
                .WithMany(t => t.SuppHotColdWorkPermitDocuments)
                .HasForeignKey(d => d.DocumentID);
            this.HasRequired(t => t.SuppHotColdWorkPermit)
                .WithMany(t => t.SuppHotColdWorkPermitDocuments)
                .HasForeignKey(d => d.SuppHotColdWorkPermitID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.SuppHotColdWorkPermitDocuments)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.SuppHotColdWorkPermitDocuments1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
