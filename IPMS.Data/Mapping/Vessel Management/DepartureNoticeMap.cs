using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class DepartureNoticeMap : EntityTypeConfiguration<DepartureNotice>
    {
        public DepartureNoticeMap()
        {
            // Primary Key
            this.HasKey(t => t.DepartureID);

            // Properties
            this.Property(t => t.VCN)
                .IsRequired()
                .HasMaxLength(12);

            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.Tidal)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DaylightRestriction)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.NoMainEngine)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.WillSheBeUnderTow)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TowingDetails)
                .HasMaxLength(500);

            this.Property(t => t.CurrentBerth)
                .HasMaxLength(50);

            this.Property(t => t.SideAlongSideCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.IsVesselDoubleBank)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.IsFinal)
                .HasMaxLength(2);

            // Table & Column Mappings
            this.ToTable("DepartureNotice");
            this.Property(t => t.DepartureID).HasColumnName("DepartureID");
            this.Property(t => t.VCN).HasColumnName("VCN");
            this.Property(t => t.VesselID).HasColumnName("VesselID");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.Tidal).HasColumnName("Tidal");
            this.Property(t => t.DaylightRestriction).HasColumnName("DaylightRestriction");
            this.Property(t => t.NoMainEngine).HasColumnName("NoMainEngine");
            this.Property(t => t.WillSheBeUnderTow).HasColumnName("WillSheBeUnderTow");
            this.Property(t => t.TowingDetails).HasColumnName("TowingDetails");
            this.Property(t => t.CurrentBerth).HasColumnName("CurrentBerth");
            this.Property(t => t.SideAlongSideCode).HasColumnName("SideAlongSideCode");
            this.Property(t => t.IsVesselDoubleBank).HasColumnName("IsVesselDoubleBank");
            this.Property(t => t.EstimatedDatetimeOfSR).HasColumnName("EstimatedDatetimeOfSR");
            this.Property(t => t.WorkflowInstanceId).HasColumnName("WorkflowInstanceId");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.IsFinal).HasColumnName("IsFinal");

            // Relationships
            this.HasRequired(t => t.Agent)
                .WithMany(t => t.DepartureNotices)
                .HasForeignKey(d => d.AgentID);
            this.HasRequired(t => t.ArrivalNotification)
                .WithMany(t => t.DepartureNotices)
                .HasForeignKey(d => d.VCN);
            this.HasRequired(t => t.Port)
                .WithMany(t => t.DepartureNotices)
                .HasForeignKey(d => d.PortCode);
            this.HasOptional(t => t.WorkflowInstance)
                .WithMany(t => t.DepartureNotices)
                .HasForeignKey(d => d.WorkflowInstanceId);
        }
    }
}
