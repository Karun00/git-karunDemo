using IPMS.Domain.Models;
using Core.Repository;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;


namespace IPMS.Data.Mapping
{
    public class OtherServiceRecordingMap : EntityTypeConfiguration<OtherServiceRecording>
    {
        public OtherServiceRecordingMap()
        {
            // Primary Key
            this.HasKey(t => t.OtherServiceRecordingID);

            // Properties
            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DelayReason)
             .HasMaxLength(500);

            this.Property(t => t.Extend)
               .IsFixedLength()
               .HasMaxLength(1);

            this.Property(t => t.PortCode)
          .HasMaxLength(2);

            this.Property(t => t.QuayCode)
                .HasMaxLength(4);

            this.Property(t => t.BerthCode)
                .HasMaxLength(4);

            this.Property(t => t.OpeningMeterReading).HasPrecision(18, 3);
            this.Property(t => t.ClosingMeterReading).HasPrecision(18, 3);

            // Table & Column Mappings
            this.ToTable("OtherServiceRecording");
            this.Property(t => t.OtherServiceRecordingID).HasColumnName("OtherServiceRecordingID");
            this.Property(t => t.ResourceAllocationID).HasColumnName("ResourceAllocationID");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
            this.Property(t => t.LineUp).HasColumnName("LineUp");
            this.Property(t => t.LineDown).HasColumnName("LineDown");
            this.Property(t => t.PilotOn).HasColumnName("PilotOn");
            this.Property(t => t.OpeningMeterReading).HasColumnName("OpeningMeterReading");
            this.Property(t => t.ClosingMeterReading).HasColumnName("ClosingMeterReading");
            this.Property(t => t.TotalDispensed).HasColumnName("TotalDispensed");
            this.Property(t => t.FirstSwing).HasColumnName("FirstSwing");
            this.Property(t => t.LastSwing).HasColumnName("LastSwing");
            this.Property(t => t.TimeAlongSide).HasColumnName("TimeAlongSide");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.BackToQuay).HasColumnName("BackToQuay");
            this.Property(t => t.Extend).HasColumnName("Extend");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.QuayCode).HasColumnName("QuayCode");
            this.Property(t => t.BerthCode).HasColumnName("BerthCode");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.Deficiencies).HasColumnName("Deficiencies");
            this.Property(t => t.WaitingStartTime).HasColumnName("WaitingStartTime");
            this.Property(t => t.WaitingEndTime).HasColumnName("WaitingEndTime");
            this.Property(t => t.DelayReason).HasColumnName("DelayReason");
            this.Property(t => t.IsCompleted).HasColumnName("IsCompleted");
            this.Property(t => t.MeterNo).HasColumnName("MeterNo");

            // Relationships
            this.HasOptional(t => t.Berth)
               .WithMany(t => t.OtherServiceRecordings)
               .HasForeignKey(d => new { d.PortCode, d.QuayCode, d.BerthCode });
            this.HasRequired(t => t.User)
                .WithMany(t => t.OtherServiceRecordings)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.OtherServiceRecordings1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.ResourceAllocation)
                .WithMany(t => t.OtherServiceRecordings)
                .HasForeignKey(d => d.ResourceAllocationID);

        }
    }
}
