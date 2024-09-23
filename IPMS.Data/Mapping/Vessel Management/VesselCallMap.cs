using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class VesselCallMap : EntityTypeConfiguration<VesselCall>
	{
		public VesselCallMap()
		{
			// Primary Key
			this.HasKey(t => t.VesselCallID);

			// Properties
			this.Property(t => t.VCN)
				.IsRequired()
                .HasMaxLength(12);

			this.Property(t => t.FromPositionPortCode)
				.IsRequired()
                .HasMaxLength(2);

			this.Property(t => t.FromPositionQuayCode)
				.IsRequired()
                .HasMaxLength(4);

			this.Property(t => t.FromPositionBerthCode)
				.IsRequired()
                .HasMaxLength(4);

			this.Property(t => t.FromPositionBollardCode)
				.IsRequired()
                .HasMaxLength(4);

			this.Property(t => t.ToPositionPortCode)
				.IsRequired()
                .HasMaxLength(2);

			this.Property(t => t.ToPositionQuayCode)
				.IsRequired()
                .HasMaxLength(4);

			this.Property(t => t.ToPositionBerthCode)
				.IsRequired()
                .HasMaxLength(4);

			this.Property(t => t.ToPositionBollardCode)
				.IsRequired()
                .HasMaxLength(4);

			this.Property(t => t.RecordStatus)
				.IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("VesselCall");
            this.Property(t => t.VesselCallID).HasColumnName("VesselCallID");
			this.Property(t => t.VCN).HasColumnName("VCN");
			this.Property(t => t.RecentAgentID).HasColumnName("RecentAgentID");
			this.Property(t => t.ETA).HasColumnName("ETA");
			this.Property(t => t.ETD).HasColumnName("ETD");
			this.Property(t => t.ETB).HasColumnName("ETB");
			this.Property(t => t.ETUB).HasColumnName("ETUB");
			this.Property(t => t.ATA).HasColumnName("ATA");
			this.Property(t => t.ATD).HasColumnName("ATD");
			this.Property(t => t.ATB).HasColumnName("ATB");
			this.Property(t => t.ATUB).HasColumnName("ATUB");
			this.Property(t => t.BreakWaterIn).HasColumnName("BreakWaterIn");
			this.Property(t => t.BreakWaterOut).HasColumnName("BreakWaterOut");
			this.Property(t => t.PortLimitIn).HasColumnName("PortLimitIn");
			this.Property(t => t.PortLimitOut).HasColumnName("PortLimitOut");
			this.Property(t => t.AnchorUp).HasColumnName("AnchorUp");
			this.Property(t => t.AnchorDown).HasColumnName("AnchorDown");
			this.Property(t => t.FromPositionPortCode).HasColumnName("FromPositionPortCode");
			this.Property(t => t.FromPositionQuayCode).HasColumnName("FromPositionQuayCode");
			this.Property(t => t.FromPositionBerthCode).HasColumnName("FromPositionBerthCode");
			this.Property(t => t.FromPositionBollardCode).HasColumnName("FromPositionBollardCode");
			this.Property(t => t.ToPositionPortCode).HasColumnName("ToPositionPortCode");
			this.Property(t => t.ToPositionQuayCode).HasColumnName("ToPositionQuayCode");
			this.Property(t => t.ToPositionBerthCode).HasColumnName("ToPositionBerthCode");
			this.Property(t => t.ToPositionBollardCode).HasColumnName("ToPositionBollardCode");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.NoofTimesETAChanged).HasColumnName("NoofTimesETAChanged");

			// Relationships
			this.HasRequired(t => t.Agent)
				.WithMany(t => t.VesselCalls)
				.HasForeignKey(d => d.RecentAgentID);
			this.HasRequired(t => t.ArrivalNotification)
				.WithMany(t => t.VesselCalls)
				.HasForeignKey(d => d.VCN);
			this.HasRequired(t => t.Bollard)
				.WithMany(t => t.VesselCalls)
				.HasForeignKey(d => new { d.FromPositionPortCode, d.FromPositionQuayCode, d.FromPositionBerthCode, d.FromPositionBollardCode });
			this.HasRequired(t => t.Bollard1)
				.WithMany(t => t.VesselCalls1)
				.HasForeignKey(d => new { d.ToPositionPortCode, d.ToPositionQuayCode, d.ToPositionBerthCode, d.ToPositionBollardCode });
			this.HasRequired(t => t.User)
				.WithMany(t => t.VesselCalls)
				.HasForeignKey(d => d.CreatedBy);
			this.HasOptional(t => t.User1)
				.WithMany(t => t.VesselCalls1)
				.HasForeignKey(d => d.ModifiedBy);

		}
	}
}
