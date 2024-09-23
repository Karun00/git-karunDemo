using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{	
    public class EventScheduleMap : EntityTypeConfiguration<EventSchedule>
    {
        public EventScheduleMap()
        {
            // Primary Key
            this.HasKey(t => t.EventScheduleID);

            // Properties
            this.Property(t => t.EventScheduleName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.EventScheduleType)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EventScheduleTime)
                .HasMaxLength(5);

            this.Property(t => t.ExecutionPlan)
                .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("EventSchedule");
            this.Property(t => t.EventScheduleID).HasColumnName("EventScheduleID");
            this.Property(t => t.EntityID).HasColumnName("EntityID");
            this.Property(t => t.EventScheduleName).HasColumnName("EventScheduleName");
            this.Property(t => t.EventScheduleType).HasColumnName("EventScheduleType");
            this.Property(t => t.EventScheduleStartDate).HasColumnName("EventScheduleStartDate");
            this.Property(t => t.EventScheduleTime).HasColumnName("EventScheduleTime");
            this.Property(t => t.ExecutionPlan).HasColumnName("ExecutionPlan");
            this.Property(t => t.NextExecutionDateTime).HasColumnName("NextExecutionDateTime");
            this.Property(t => t.EventScheduleEndDateTime).HasColumnName("EventScheduleEndDateTime");
            this.Property(t => t.ExecutionCount).HasColumnName("ExecutionCount");
            this.Property(t => t.LastExecutionDateTime).HasColumnName("LastExecutionDateTime");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasOptional(t => t.Entity)
                .WithMany(t => t.EventSchedules)
                .HasForeignKey(d => d.EntityID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.EventSchedules)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.EventSchedules1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
