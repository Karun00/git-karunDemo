using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class VesselHatchHoldMap : EntityTypeConfiguration<VesselHatchHold>
	{
		public VesselHatchHoldMap()
		{
			// Primary Key
			this.HasKey(t => t.VesselHatchHoldID);

			// Properties
			this.Property(t => t.Description)
				.HasMaxLength(200);

			this.Property(t => t.RecordStatus)
				.IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("VesselHatchHold");
			this.Property(t => t.VesselHatchHoldID).HasColumnName("VesselHatchHoldID");
			this.Property(t => t.VesselID).HasColumnName("VesselID");
			this.Property(t => t.HatchHoldTypeM).HasColumnName("HatchHoldTypeM");
			this.Property(t => t.SafeWorkingLoad).HasColumnName("SafeWorkingLoad");
			this.Property(t => t.HoldCapacityCBM).HasColumnName("HoldCapacityCBM");
			this.Property(t => t.Description).HasColumnName("Description");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

			// Relationships
			this.HasRequired(t => t.User)
				.WithMany(t => t.VesselHatchHolds)
				.HasForeignKey(d => d.CreatedBy);
			this.HasRequired(t => t.User1)
				.WithMany(t => t.VesselHatchHolds1)
				.HasForeignKey(d => d.ModifiedBy);
			this.HasRequired(t => t.Vessel)
				.WithMany(t => t.VesselHatchHolds)
				.HasForeignKey(d => d.VesselID);

		}
	}
}
