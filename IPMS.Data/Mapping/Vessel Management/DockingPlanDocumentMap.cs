using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class DockingPlanDocumentMap : EntityTypeConfiguration<DockingPlanDocument>
    {
        public DockingPlanDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.DockingPlanDocumentID);

            // Properties
            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("DockingPlanDocument");
            this.Property(t => t.DockingPlanID).HasColumnName("DockingPlanID");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.DockingPlanDocumentID).HasColumnName("DockingPlanDocumentID");

            // Relationships
            this.HasRequired(t => t.DockingPlan)
                .WithMany(t => t.DockingPlanDocuments)
                .HasForeignKey(d => d.DockingPlanID);
            this.HasRequired(t => t.Document)
                .WithMany(t => t.DockingPlanDocuments)
                .HasForeignKey(d => d.DocumentID);

        }
    }
}
