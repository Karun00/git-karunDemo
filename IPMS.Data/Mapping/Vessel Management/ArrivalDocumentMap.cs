using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class ArrivalDocumentMap : EntityTypeConfiguration<ArrivalDocument>
    {
        public ArrivalDocumentMap()
        {
            // Primary Key
            this.HasKey(t => new { t.VCN, t.DocumentID });

            // Properties
            this.Property(t => t.VCN)
                .IsRequired()
                .HasMaxLength(12);

            this.Property(t => t.DocumentCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.FileName)
           .IsRequired();

            this.Property(t => t.DocumentID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("ArrivalDocument");
            this.Property(t => t.VCN).HasColumnName("VCN");
            this.Property(t => t.DocumentCode).HasColumnName("DocumentCode");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.ArrivalDocuments) 
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.Document)
                .WithMany(t => t.ArrivalDocuments)
                .HasForeignKey(d => d.DocumentID);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.ArrivalDocuments1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.ArrivalNotification)
                .WithMany(t => t.ArrivalDocuments)
                .HasForeignKey(d => d.VCN);

            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.ArvDocument)
                .HasForeignKey(d => d.DocumentCode);


        }
    }
}
