using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class AgentDocumentMap : EntityTypeConfiguration<AgentDocument>
    {
        public AgentDocumentMap()
        {
            // Primary Key
            this.HasKey(t => new { t.AgentID, t.DocumentID, t.CreatedBy });

            // Properties
            this.Property(t => t.AgentID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DocumentID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RecordStatus)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CreatedBy)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("AgentDocument");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Agent)
                .WithMany(t => t.AgentDocuments)
                .HasForeignKey(d => d.AgentID);
            this.HasRequired(t => t.Document)
                .WithMany(t => t.AgentDocuments)
                .HasForeignKey(d => d.DocumentID);

        }
    }
}
