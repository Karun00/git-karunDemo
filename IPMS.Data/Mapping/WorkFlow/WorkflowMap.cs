using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class WorkflowMap : EntityTypeConfiguration<Workflow>
	{
		public WorkflowMap()
		{
			// Primary Key
			this.HasKey(t => t.WorkflowCode);

			// Properties
			this.Property(t => t.WorkflowCode)
				.IsRequired()
                .HasMaxLength(4);

			this.Property(t => t.WorkflowName)
				.IsRequired()
                .HasMaxLength(50);

			this.Property(t => t.RecordStatus)
				.IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("Workflow");
			this.Property(t => t.WorkflowCode).HasColumnName("WorkflowCode");
			this.Property(t => t.WorkflowName).HasColumnName("WorkflowName");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

			// Relationships
			this.HasRequired(t => t.User)
				.WithMany(t => t.Workflows)
				.HasForeignKey(d => d.CreatedBy);
			this.HasRequired(t => t.User1)
				.WithMany(t => t.Workflows1)
				.HasForeignKey(d => d.ModifiedBy);

		}
	}
}
