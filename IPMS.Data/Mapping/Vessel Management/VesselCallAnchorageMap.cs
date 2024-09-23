using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class VesselCallAnchorageMap : EntityTypeConfiguration<VesselCallAnchorage>
	{
		public VesselCallAnchorageMap()
		{
			// Primary Key
			this.HasKey(t => t.VesselCallAnchorageID);

			// Properties
			this.Property(t => t.VCN)
				.IsRequired()
                .HasMaxLength(12);

			this.Property(t => t.AnchorPosition)
				.IsRequired()
                .HasMaxLength(50);

			this.Property(t => t.BearingDistanceFromBreakWater)
				.IsRequired()
                .HasMaxLength(50);

			this.Property(t => t.Reason)
				.HasMaxLength(4);

			this.Property(t => t.Remarks)
				.HasMaxLength(2000);

			this.Property(t => t.RecordStatus)
				.IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("VesselCallAnchorage");
			this.Property(t => t.VesselCallAnchorageID).HasColumnName("VesselCallAnchorageID");
			this.Property(t => t.VCN).HasColumnName("VCN");
			this.Property(t => t.AnchorDropTime).HasColumnName("AnchorDropTime");
			this.Property(t => t.AnchorAweighTime).HasColumnName("AnchorAweighTime");
			this.Property(t => t.AnchorPosition).HasColumnName("AnchorPosition");
			this.Property(t => t.BearingDistanceFromBreakWater).HasColumnName("BearingDistanceFromBreakWater");
			this.Property(t => t.Reason).HasColumnName("Reason");
			this.Property(t => t.Remarks).HasColumnName("Remarks");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

			// Relationships
			this.HasRequired(t => t.ArrivalNotification)
				.WithMany(t => t.VesselCallAnchorages)
				.HasForeignKey(d => d.VCN);
			this.HasOptional(t => t.SubCategory)
				.WithMany(t => t.VesselCallAnchorages)
				.HasForeignKey(d => d.Reason);
			this.HasRequired(t => t.User)
				.WithMany(t => t.VesselCallAnchorages)
				.HasForeignKey(d => d.CreatedBy);
			this.HasRequired(t => t.User1)
				.WithMany(t => t.VesselCallAnchorages1)
				.HasForeignKey(d => d.ModifiedBy);

		}
	}
}
