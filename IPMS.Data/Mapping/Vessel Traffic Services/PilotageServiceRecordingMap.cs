using IPMS.Domain.Models;
using Core.Repository;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;


namespace IPMS.Data.Mapping
{
    public class PilotageServiceRecordingMap : EntityTypeConfiguration<PilotageServiceRecording>
    {
        public PilotageServiceRecordingMap()
        {
            // Primary Key
            this.HasKey(t => t.PilotageServiceRecordingID);

            // Properties
            this.Property(t => t.AdditionalTugs)
                .HasMaxLength(5);

            this.Property(t => t.OffSteam)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DelayReason)
             .HasMaxLength(500);

            this.Property(t => t.MarineRevenueCleared)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Remarks)
                .HasMaxLength(500);

            this.Property(t => t.Deficiencies)
                .HasMaxLength(500);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("PilotageServiceRecording");
            this.Property(t => t.PilotageServiceRecordingID).HasColumnName("PilotageServiceRecordingID");
            this.Property(t => t.ResourceAllocationID).HasColumnName("ResourceAllocationID");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
            this.Property(t => t.PilotOnBoard).HasColumnName("PilotOnBoard");
            this.Property(t => t.PilotOff).HasColumnName("PilotOff");
            this.Property(t => t.WaitingStartTime).HasColumnName("WaitingStartTime");
            this.Property(t => t.WaitingEndTime).HasColumnName("WaitingEndTime");
            this.Property(t => t.AdditionalTugs).HasColumnName("AdditionalTugs");
            this.Property(t => t.OffSteam).HasColumnName("OffSteam");
            this.Property(t => t.MarineRevenueCleared).HasColumnName("MarineRevenueCleared");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.Deficiencies).HasColumnName("Deficiencies");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.DelayReason).HasColumnName("DelayReason");
            this.Property(t => t.MOPSDelay).HasColumnName("MOPSDelay");

            // Relationships
            this.HasRequired(t => t.SubCategory)
               .WithMany(t => t.PilotageServiceRecordings)
               .HasForeignKey(d => d.MOPSDelay);
            this.HasRequired(t => t.User)
                .WithMany(t => t.PilotageServiceRecordings)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.PilotageServiceRecordings1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.ResourceAllocation)
                .WithMany(t => t.PilotageServiceRecordings)
                .HasForeignKey(d => d.ResourceAllocationID);

        }
    }
}
