using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class VesselAgentChangeDocumentMap : EntityTypeConfiguration<VesselAgentChangeDocument>
	{
		public VesselAgentChangeDocumentMap()
		{
			// Primary Key
			this.HasKey(t => new { t.VesselAgentChangeID, t.DocumentID });

			// Properties
			this.Property(t => t.VesselAgentChangeID)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			this.Property(t => t.DocumentID)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			this.Property(t => t.RecordStatus)
				.IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DocumentName)
                .HasMaxLength(100);

			// Table & Column Mappings
			this.ToTable("VesselAgentChangeDocument");
			this.Property(t => t.VesselAgentChangeID).HasColumnName("VesselAgentChangeID");
			this.Property(t => t.DocumentID).HasColumnName("DocumentID");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.DocumentName).HasColumnName("DocumentName");
            this.Property(t => t.FileName).HasColumnName("FileName");

			// Relationships
			this.HasRequired(t => t.Document)
				.WithMany(t => t.VesselAgentChangeDocuments)
				.HasForeignKey(d => d.DocumentID);
			this.HasRequired(t => t.User)
				.WithMany(t => t.VesselAgentChangeDocuments)
				.HasForeignKey(d => d.CreatedBy);
			this.HasRequired(t => t.User1)
				.WithMany(t => t.VesselAgentChangeDocuments1)
				.HasForeignKey(d => d.ModifiedBy);
			this.HasRequired(t => t.VesselAgentChange)
				.WithMany(t => t.VesselAgentChangeDocuments)
				.HasForeignKey(d => d.VesselAgentChangeID);

		}
	}
}
