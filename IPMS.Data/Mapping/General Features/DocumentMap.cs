using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class DocumentMap : EntityTypeConfiguration<Document>
    {
        public DocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.DocumentID);

            // Properties
            this.Property(t => t.DocumentType)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.DocumentName)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.DocumentPath)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.FileName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Document");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.DocumentType).HasColumnName("DocumentType");
            this.Property(t => t.DocumentName).HasColumnName("DocumentName");
            this.Property(t => t.DocumentPath).HasColumnName("DocumentPath");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.FileType).HasColumnName("FileType");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Data).HasColumnName("Data");
            // Relationships
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.Documents)
                .HasForeignKey(d => d.DocumentName);

            this.HasRequired(t => t.SubCategory1)
                .WithMany(t => t.Documents1)
                .HasForeignKey(d => d.DocumentType);


        }
    }
}
