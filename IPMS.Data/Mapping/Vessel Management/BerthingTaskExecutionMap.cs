using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class BerthingTaskExecutionMap : EntityTypeConfiguration<BerthingTaskExecution>
	{
		public BerthingTaskExecutionMap()
		{
			// Primary Key
			this.HasKey(t => t.BerthingTaskExecutionID);

			// Properties
			this.Property(t => t.BerthingSide)
				.HasMaxLength(4);

			this.Property(t => t.Remarks)
				.HasMaxLength(200);

			this.Property(t => t.RecordStatus)
				.IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("BerthingTaskExecution");
			this.Property(t => t.BerthingTaskExecutionID).HasColumnName("BerthingTaskExecutionID");
			this.Property(t => t.MovementResourceAllocationID).HasColumnName("MovementResourceAllocationID");
			this.Property(t => t.StartTime).HasColumnName("StartTime");
			this.Property(t => t.EndTime).HasColumnName("EndTime");
			this.Property(t => t.BerthingSide).HasColumnName("BerthingSide");
			this.Property(t => t.Remarks).HasColumnName("Remarks");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

			// Relationships
			this.HasRequired(t => t.User)
				.WithMany(t => t.BerthingTaskExecutions)
				.HasForeignKey(d => d.CreatedBy);
			this.HasRequired(t => t.User1)
				.WithMany(t => t.BerthingTaskExecutions1)
				.HasForeignKey(d => d.ModifiedBy);
			this.HasRequired(t => t.MovementResourceAllocation)
				.WithMany(t => t.BerthingTaskExecutions)
				.HasForeignKey(d => d.MovementResourceAllocationID);

		}
	}
}
