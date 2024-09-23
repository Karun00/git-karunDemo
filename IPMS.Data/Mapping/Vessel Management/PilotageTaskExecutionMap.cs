using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class PilotageTaskExecutionMap : EntityTypeConfiguration<PilotageTaskExecution>
	{
		public PilotageTaskExecutionMap()
		{
			// Primary Key
			this.HasKey(t => t.PilotageTaskExecutionID);

			// Properties
			this.Property(t => t.OffSteam)
				.IsFixedLength()
                .HasMaxLength(1);

			this.Property(t => t.MarineRevenueCleared)
				.IsFixedLength()
                .HasMaxLength(1);

			this.Property(t => t.Remarks)
				.HasMaxLength(200);

			this.Property(t => t.Deficiencies)
				.HasMaxLength(200);

			this.Property(t => t.RecordStatus)
				.IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("PilotageTaskExecution");
			this.Property(t => t.PilotageTaskExecutionID).HasColumnName("PilotageTaskExecutionID");
			this.Property(t => t.MovementResourceAllocationID).HasColumnName("MovementResourceAllocationID");
			this.Property(t => t.StartTime).HasColumnName("StartTime");
			this.Property(t => t.PilotOnBoard).HasColumnName("PilotOnBoard");
			this.Property(t => t.PilotOff).HasColumnName("PilotOff");
			this.Property(t => t.EndTime).HasColumnName("EndTime");
			this.Property(t => t.WaitingStartTime).HasColumnName("WaitingStartTime");
			this.Property(t => t.WaitingEndTime).HasColumnName("WaitingEndTime");
			this.Property(t => t.AdditionalTugs).HasColumnName("AdditionalTugs");
			this.Property(t => t.OffSteam).HasColumnName("OffSteam");
			this.Property(t => t.MarineRevenueCleared).HasColumnName("MarineRevenueCleared");
			this.Property(t => t.Remarks).HasColumnName("Remarks");
			this.Property(t => t.Deficiencies).HasColumnName("Deficiencies");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

			// Relationships
			this.HasRequired(t => t.MovementResourceAllocation)
				.WithMany(t => t.PilotageTaskExecutions)
				.HasForeignKey(d => d.MovementResourceAllocationID);
			this.HasRequired(t => t.User)
				.WithMany(t => t.PilotageTaskExecutions)
				.HasForeignKey(d => d.CreatedBy);
			this.HasRequired(t => t.User1)
				.WithMany(t => t.PilotageTaskExecutions1)
				.HasForeignKey(d => d.ModifiedBy);

		}
	}
}
