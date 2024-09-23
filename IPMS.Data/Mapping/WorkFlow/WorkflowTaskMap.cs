using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class WorkflowTaskMap : EntityTypeConfiguration<WorkflowTask>
    {
        public WorkflowTaskMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EntityID, t.WorkflowTaskCode, t.Step, t.PortCode });

            // Properties
            this.Property(t => t.EntityID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.WorkflowTaskCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.Step)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.HasNotification)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.APIUrl)
                .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            // Table & Column Mappings
            this.ToTable("WorkflowTask");
            this.Property(t => t.EntityID).HasColumnName("EntityID");
            this.Property(t => t.WorkflowTaskCode).HasColumnName("WorkflowTaskCode");
            this.Property(t => t.Step).HasColumnName("Step");
            this.Property(t => t.NextStep).HasColumnName("NextStep");
            this.Property(t => t.ValidityPeriod).HasColumnName("ValidityPeriod");
            this.Property(t => t.HasNotification).HasColumnName("HasNotification");
            this.Property(t => t.APIUrl).HasColumnName("APIUrl");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.HasRemarks).HasColumnName("HasRemarks");

            // Relationships
            this.HasRequired(t => t.Entity)
                .WithMany(t => t.WorkflowTasks)
                .HasForeignKey(d => d.EntityID);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.WorkflowTasks)
                .HasForeignKey(d => d.WorkflowTaskCode);
            this.HasRequired(t => t.User)
                .WithMany(t => t.WorkflowTasks)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.WorkflowTasks1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.Port)
                .WithMany(t => t.WorkflowTask1)
                .HasForeignKey(d => d.PortCode);
        }
    }
}
