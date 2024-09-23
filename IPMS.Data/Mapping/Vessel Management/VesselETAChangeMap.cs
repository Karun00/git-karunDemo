using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
	public class VesselETAChangeMap : EntityTypeConfiguration<VesselETAChange>
	{
		public VesselETAChangeMap()
		{
			// Primary Key
			this.HasKey(t => t.VesselETAChangeID);

			// Properties
			this.Property(t => t.VCN)
				.IsRequired()
                .HasMaxLength(12);

			this.Property(t => t.VoyageIn)
				.HasMaxLength(50);

			this.Property(t => t.VoyageOut)
				.HasMaxLength(50);

			this.Property(t => t.Remarks)
				.HasMaxLength(200);

			this.Property(t => t.RecordStatus)
				.IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("VesselETAChange");
			this.Property(t => t.VesselETAChangeID).HasColumnName("VesselETAChangeID");
			this.Property(t => t.VCN).HasColumnName("VCN");
			this.Property(t => t.VoyageIn).HasColumnName("VoyageIn");
			this.Property(t => t.VoyageOut).HasColumnName("VoyageOut");
			this.Property(t => t.ETA).HasColumnName("ETA");
			this.Property(t => t.ETD).HasColumnName("ETD");
			this.Property(t => t.Remarks).HasColumnName("Remarks");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.OldETA).HasColumnName("OldETA");
            this.Property(t => t.OldETD).HasColumnName("OldETD");

            this.Property(t => t.PlanDateTimeOfBerth).HasColumnName("PlanDateTimeOfBerth");
            this.Property(t => t.PlanDateTimeToStartCargo).HasColumnName("PlanDateTimeToStartCargo");
            this.Property(t => t.PlanDateTimeToCompleteCargo).HasColumnName("PlanDateTimeToCompleteCargo");
            this.Property(t => t.PlanDateTimeToVacateBerth).HasColumnName("PlanDateTimeToVacateBerth");
            this.Property(t => t.OldPlanDateTimeOfBerth).HasColumnName("OldPlanDateTimeOfBerth");
            this.Property(t => t.OldPlanDateTimeToStartCargo).HasColumnName("OldPlanDateTimeToStartCargo");
            this.Property(t => t.OldPlanDateTimeToCompleteCargo).HasColumnName("OldPlanDateTimeToCompleteCargo");
            this.Property(t => t.OldPlanDateTimeToVacateBerth).HasColumnName("OldPlanDateTimeToVacateBerth");

			// Relationships
			this.HasRequired(t => t.ArrivalNotification)
				.WithMany(t => t.VesselETAChanges)
				.HasForeignKey(d => d.VCN);
			this.HasRequired(t => t.User)
				.WithMany(t => t.VesselETAChanges)
				.HasForeignKey(d => d.CreatedBy);
			this.HasRequired(t => t.User1)
				.WithMany(t => t.VesselETAChanges1)
				.HasForeignKey(d => d.ModifiedBy);

		}
	}
}
