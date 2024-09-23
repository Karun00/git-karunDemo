using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class WorkflowProcessMap : EntityTypeConfiguration<WorkflowProcess>
	{
        public WorkflowProcessMap()
        {
            // Primary Key
            this.HasKey(t => new { t.WorkflowProcessId });

            // Properties
            this.Property(t => t.WorkflowProcessId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.RoleId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FromTaskCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.ToTaskCode)
                .HasMaxLength(4);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CreatedBy)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("WorkflowProcess");
            this.Property(t => t.WorkflowProcessId).HasColumnName("WorkflowProcessId");
            this.Property(t => t.WorkflowInstanceId).HasColumnName("WorkflowInstanceId");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            this.Property(t => t.FromTaskCode).HasColumnName("FromTaskCode");
            this.Property(t => t.ToTaskCode).HasColumnName("ToTaskCode");
            this.Property(t => t.ReferenceData).HasColumnName("ReferenceData");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Role)
                .WithMany(t => t.WorkflowProcess)
                .HasForeignKey(d => d.RoleId);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.WorkflowProcess)
                .HasForeignKey(d => d.FromTaskCode);
            this.HasOptional(t => t.SubCategory1)
                .WithMany(t => t.WorkflowProcess1)
                .HasForeignKey(d => d.ToTaskCode);
            this.HasRequired(t => t.User)
                .WithMany(t => t.WorkflowProcess)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.WorkflowProcess1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasOptional(t => t.WorkflowInstance)
                .WithMany(t => t.WorkflowProcess)
                .HasForeignKey(d => d.WorkflowInstanceId);
        }
    }
}
