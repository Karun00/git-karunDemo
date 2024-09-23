using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;


namespace IPMS.Data.Mapping
{
    public class BerthMaintenanceCompletionMap : EntityTypeConfiguration<BerthMaintenanceCompletion>
    {
        public BerthMaintenanceCompletionMap()
        {
            // Primary Key
            this.HasKey(t => t.BerthMaintenanceCompletionID);

            // Properties
            this.Property(t => t.observation)
                .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("BerthMaintenanceCompletion");
            this.Property(t => t.BerthMaintenanceCompletionID).HasColumnName("BerthMaintenanceCompletionID");
            this.Property(t => t.BerthMaintenanceID).HasColumnName("BerthMaintenanceID");
            this.Property(t => t.CompletionDateTime).HasColumnName("CompletionDateTime");
            this.Property(t => t.observation).HasColumnName("observation");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.WorkflowInstanceId).HasColumnName("WorkflowInstanceId");

            // Relationships
            this.HasRequired(t => t.BerthMaintenance)
                .WithMany(t => t.BerthMaintenanceCompletions)
                .HasForeignKey(d => d.BerthMaintenanceID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.BerthMaintenanceCompletions)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.BerthMaintenanceCompletions1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasOptional(t => t.WorkflowInstance)
                .WithMany(t => t.BerthMaintenanceCompletions)
                .HasForeignKey(d => d.WorkflowInstanceId);

        }
    }
}
