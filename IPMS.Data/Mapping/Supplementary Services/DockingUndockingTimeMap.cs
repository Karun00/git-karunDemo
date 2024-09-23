using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class DockingUndockingTimeMap : EntityTypeConfiguration<DockingUndockingTime>
    {
        public DockingUndockingTimeMap()
        {
            // Primary Key
            this.HasKey(t => t.DockingUndockingTimeID);

            // Properties
            this.Property(t => t.Chamber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("DockingUndockingTime");
            this.Property(t => t.DockingUndockingTimeID).HasColumnName("DockingUndockingTimeID");
            this.Property(t => t.Chamber).HasColumnName("Chamber");
            this.Property(t => t.VesselEnteredDockAt).HasColumnName("VesselEnteredDockAt");
            this.Property(t => t.OnBlocks).HasColumnName("OnBlocks");
            this.Property(t => t.DryDockAt).HasColumnName("DryDockAt");
            this.Property(t => t.FinishedWithDockAt).HasColumnName("FinishedWithDockAt");
            this.Property(t => t.OffBlocks).HasColumnName("OffBlocks");
            this.Property(t => t.VesselLeftDockAt).HasColumnName("VesselLeftDockAt");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.DockingUndockingTimes)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.DockingUndockingTimes1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
