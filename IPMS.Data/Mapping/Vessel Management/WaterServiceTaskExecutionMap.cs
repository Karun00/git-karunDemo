using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class WaterServiceTaskExecutionMap : EntityTypeConfiguration<WaterServiceTaskExecution>
	{
		public WaterServiceTaskExecutionMap()
		{
			// Primary Key
			this.HasKey(t => t.WaterServiceTaskExecutionId);

			// Properties
			this.Property(t => t.MeterSerialNumber)
				.IsRequired()
                .HasMaxLength(20);

			this.Property(t => t.Remarks)
				.HasMaxLength(200);

			this.Property(t => t.Deficiencies)
				.HasMaxLength(200);

			this.Property(t => t.RecordStatus)
				.IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("WaterServiceTaskExecution");
			this.Property(t => t.WaterServiceTaskExecutionId).HasColumnName("WaterServiceTaskExecutionId");
			this.Property(t => t.MovementResourceAllocationID).HasColumnName("MovementResourceAllocationID");
			this.Property(t => t.StartTime).HasColumnName("StartTime");
			this.Property(t => t.MeterSerialNumber).HasColumnName("MeterSerialNumber");
			this.Property(t => t.OpeningMeterReading).HasColumnName("OpeningMeterReading");
			this.Property(t => t.ClosingMeterReading).HasColumnName("ClosingMeterReading");
			this.Property(t => t.EndTime).HasColumnName("EndTime");
			this.Property(t => t.Remarks).HasColumnName("Remarks");
			this.Property(t => t.Deficiencies).HasColumnName("Deficiencies");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

			// Relationships
			this.HasRequired(t => t.MovementResourceAllocation)
				.WithMany(t => t.WaterServiceTaskExecutions)
				.HasForeignKey(d => d.MovementResourceAllocationID);
			this.HasRequired(t => t.User)
				.WithMany(t => t.WaterServiceTaskExecutions)
				.HasForeignKey(d => d.CreatedBy);
			this.HasRequired(t => t.User1)
				.WithMany(t => t.WaterServiceTaskExecutions1)
				.HasForeignKey(d => d.ModifiedBy);

		}
	}
}
