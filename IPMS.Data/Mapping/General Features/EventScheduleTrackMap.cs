using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class EventScheduleTrackMap : EntityTypeConfiguration<EventScheduleTrack>
    {
        public EventScheduleTrackMap()
        {
            // Primary Key
            this.HasKey(t => t.EventScheduleTrackID);

            // Properties
            this.Property(t => t.Reference)
                .HasMaxLength(12);

            // Table & Column Mappings
            this.ToTable("EventScheduleTrack");
            this.Property(t => t.EventScheduleTrackID).HasColumnName("EventScheduleTrackID");
            this.Property(t => t.EventScheduleTaskID).HasColumnName("EventScheduleTaskID");
            this.Property(t => t.Reference).HasColumnName("Reference");
            this.Property(t => t.NotificationId).HasColumnName("NotificationId");
            this.Property(t => t.WorkflowInstanceId).HasColumnName("WorkflowInstanceId");
            this.Property(t => t.WorkflowProcessId).HasColumnName("WorkflowProcessId");

            // Relationships
            this.HasOptional(t => t.EventScheduleTask)
                .WithMany(t => t.EventScheduleTracks)
                .HasForeignKey(d => d.EventScheduleTaskID);
            this.HasOptional(t => t.Notification)
                .WithMany(t => t.EventScheduleTracks)
                .HasForeignKey(d => d.NotificationId);
            this.HasOptional(t => t.WorkflowInstance)
                .WithMany(t => t.EventScheduleTracks)
                .HasForeignKey(d => d.WorkflowInstanceId);
            this.HasOptional(t => t.WorkflowProcess)
                .WithMany(t => t.EventScheduleTracks)
                .HasForeignKey(d => d.WorkflowProcessId);

        }
    }
}
