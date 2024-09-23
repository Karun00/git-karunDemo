using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class BerthMaintenanceApprovalMap : EntityTypeConfiguration<BerthMaintenanceApproval>
    {
        public BerthMaintenanceApprovalMap()
        {
            // Primary Key
            this.HasKey(t => t.BerthMaintenanceApprovalID);

            // Properties
            this.Property(t => t.WFStatus)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.RejectComments)
                .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("BerthMaintenanceApproval");
            this.Property(t => t.BerthMaintenanceApprovalID).HasColumnName("BerthMaintenanceApprovalID");
            this.Property(t => t.BerthMaintenanceID).HasColumnName("BerthMaintenanceID");
            this.Property(t => t.WFStatus).HasColumnName("WFStatus");
            this.Property(t => t.ApprovedBy).HasColumnName("ApprovedBy");
            this.Property(t => t.ApprovedDate).HasColumnName("ApprovedDate");
            this.Property(t => t.RejectComments).HasColumnName("RejectComments");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.BerthMaintenance)
                .WithMany(t => t.BerthMaintenanceApprovals)
                .HasForeignKey(d => d.BerthMaintenanceID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.BerthMaintenanceApprovals)
                .HasForeignKey(d => d.ApprovedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.BerthMaintenanceApprovals1)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User2)
                .WithMany(t => t.BerthMaintenanceApprovals2)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.BerthMaintenanceApprovals)
                .HasForeignKey(d => d.WFStatus);

        }
    }
}
