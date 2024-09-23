using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class FloatingCraneTaskExecutionMap : EntityTypeConfiguration<FloatingCraneTaskExecution>
	{
		public FloatingCraneTaskExecutionMap()
		{
			// Primary Key
			this.HasKey(t => t.FloatingCraneTaskExecutionID);

			// Properties
			this.Property(t => t.Remarks)
				.HasMaxLength(200);

			this.Property(t => t.RecordStatus)
				.IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("FloatingCraneTaskExecution");
			this.Property(t => t.FloatingCraneTaskExecutionID).HasColumnName("FloatingCraneTaskExecutionID");
			this.Property(t => t.MovementResourceAllocationID).HasColumnName("MovementResourceAllocationID");
			this.Property(t => t.StartTime).HasColumnName("StartTime");
			this.Property(t => t.TimeAlongside).HasColumnName("TimeAlongside");
			this.Property(t => t.FirstSwing).HasColumnName("FirstSwing");
			this.Property(t => t.LastSwing).HasColumnName("LastSwing");
			this.Property(t => t.EndTime).HasColumnName("EndTime");
			this.Property(t => t.Remarks).HasColumnName("Remarks");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

			// Relationships
			this.HasRequired(t => t.User)
				.WithMany(t => t.FloatingCraneTaskExecutions)
				.HasForeignKey(d => d.CreatedBy);
			this.HasRequired(t => t.User1)
				.WithMany(t => t.FloatingCraneTaskExecutions1)
				.HasForeignKey(d => d.ModifiedBy);
			this.HasRequired(t => t.MovementResourceAllocation)
				.WithMany(t => t.FloatingCraneTaskExecutions)
				.HasForeignKey(d => d.MovementResourceAllocationID);

		}
	}
}
