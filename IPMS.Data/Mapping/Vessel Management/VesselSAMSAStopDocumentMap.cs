using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class VesselSAMSAStopDocumentMap : EntityTypeConfiguration<VesselSAMSAStopDocument>
    {
        public VesselSAMSAStopDocumentMap()
        {
            // Primary Key
            this.HasKey(t => new { t.VAISID, t.DocumentID });

            // Properties
            this.Property(t => t.VAISID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DocumentID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("VesselSAMSAStopDocument");
            this.Property(t => t.VAISID).HasColumnName("VAISID");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Document)
                .WithMany(t => t.VesselSAMSAStopDocuments)
                .HasForeignKey(d => d.DocumentID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.VesselSAMSAStopDocuments)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.VesselSAMSAStopDocuments1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.VesselArrestImmobilizationSAMSA)
                .WithMany(t => t.VesselSAMSAStopDocuments)
                .HasForeignKey(d => d.VAISID);

        }
    }
}
