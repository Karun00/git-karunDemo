using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class VesselGrabMap : EntityTypeConfiguration<VesselGrab>
	{
		public VesselGrabMap()
		{
			// Primary Key
			this.HasKey(t => t.VesselGrabID);

			// Properties
			this.Property(t => t.Description)
				.HasMaxLength(200);

			this.Property(t => t.RecordStatus)
				.IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("VesselGrab");
			this.Property(t => t.VesselGrabID).HasColumnName("VesselGrabID");
			this.Property(t => t.VesselID).HasColumnName("VesselID");
			this.Property(t => t.GrabTypeM).HasColumnName("GrabTypeM");
			this.Property(t => t.SafeWorkingLoad).HasColumnName("SafeWorkingLoad");
			this.Property(t => t.GrabCapacityCBM).HasColumnName("GrabCapacityCBM");
			this.Property(t => t.Description).HasColumnName("Description");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

			// Relationships
			this.HasRequired(t => t.User)
				.WithMany(t => t.VesselGrabs)
				.HasForeignKey(d => d.CreatedBy);
			this.HasRequired(t => t.User1)
				.WithMany(t => t.VesselGrabs1)
				.HasForeignKey(d => d.ModifiedBy);
			this.HasRequired(t => t.Vessel)
				.WithMany(t => t.VesselGrabs)
				.HasForeignKey(d => d.VesselID);

		}
	}
}
