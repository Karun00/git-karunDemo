using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class VesselGearMap : EntityTypeConfiguration<VesselGear>
	{
		public VesselGearMap()
		{
			// Primary Key
			this.HasKey(t => t.VesselGearID);

			// Properties
			this.Property(t => t.Description)
				.HasMaxLength(200);

			this.Property(t => t.RecordStatus)
				.IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("VesselGear");
			this.Property(t => t.VesselGearID).HasColumnName("VesselGearID");
			this.Property(t => t.VesselID).HasColumnName("VesselID");
			this.Property(t => t.GearTypeM).HasColumnName("GearTypeM");
			this.Property(t => t.SafeWorkingLoad).HasColumnName("SafeWorkingLoad");
			this.Property(t => t.GearCapacityCBM).HasColumnName("GearCapacityCBM");
			this.Property(t => t.Description).HasColumnName("Description");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

			// Relationships
			this.HasRequired(t => t.User)
				.WithMany(t => t.VesselGears)
				.HasForeignKey(d => d.CreatedBy);
			this.HasRequired(t => t.User1)
				.WithMany(t => t.VesselGears1)
				.HasForeignKey(d => d.ModifiedBy);
			this.HasRequired(t => t.Vessel)
				.WithMany(t => t.VesselGears)
				.HasForeignKey(d => d.VesselID);

		}
	}
}
