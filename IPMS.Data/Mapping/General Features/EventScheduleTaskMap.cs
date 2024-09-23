using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{	
    public class EventScheduleTaskMap : EntityTypeConfiguration<EventScheduleTask>
    {
        public EventScheduleTaskMap()
        {
            // Primary Key
            this.HasKey(t => t.EventScheduleTaskID);

            // Properties
            this.Property(t => t.EventScheduleTaskName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.EventScheduleTaskDescription)
                .HasMaxLength(2000);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("EventScheduleTask");
            this.Property(t => t.EventScheduleTaskID).HasColumnName("EventScheduleTaskID");
            this.Property(t => t.EventScheduleID).HasColumnName("EventScheduleID");
            this.Property(t => t.SequenceID).HasColumnName("SequenceID");
            this.Property(t => t.EventScheduleTaskName).HasColumnName("EventScheduleTaskName");
            this.Property(t => t.EventScheduleTaskDescription).HasColumnName("EventScheduleTaskDescription");
            this.Property(t => t.EventScheduleParameter).HasColumnName("EventScheduleParameter");
            this.Property(t => t.EventScheduleParameterValues).HasColumnName("EventScheduleParameterValues");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.EventSchedule)
                .WithMany(t => t.EventScheduleTasks)
                .HasForeignKey(d => d.EventScheduleID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.EventScheduleTasks)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.EventScheduleTasks1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
