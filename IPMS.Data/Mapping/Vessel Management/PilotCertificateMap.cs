using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class PilotCertificateMap : EntityTypeConfiguration<PilotCertificate>
    {
        public PilotCertificateMap()
        {
            // Primary Key
            this.HasKey(t => t.PilotCertificateID);

            // Properties
            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("PilotCertificate");
            this.Property(t => t.PilotCertificateID).HasColumnName("PilotCertificateID");
            this.Property(t => t.PilotID).HasColumnName("PilotID");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Document)
                .WithMany(t => t.PilotCertificates)
                .HasForeignKey(d => d.DocumentID);
            this.HasRequired(t => t.Pilot)
                .WithMany(t => t.PilotCertificates)
                .HasForeignKey(d => d.PilotID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.PilotCertificates)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.PilotCertificates1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
