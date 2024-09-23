using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class WorkflowStepMap : EntityTypeConfiguration<WorkflowStep>
	{
		public WorkflowStepMap()
		{
			// Primary Key
            this.HasKey(t => new { t.EntityID, t.Step });

            // Properties
            this.Property(t => t.EntityID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Step)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("WorkflowStep");
            this.Property(t => t.EntityID).HasColumnName("EntityID");
            this.Property(t => t.Step).HasColumnName("Step");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.WorkflowSteps)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.WorkflowSteps1)
                .HasForeignKey(d => d.ModifiedBy);

		}
	}
}
