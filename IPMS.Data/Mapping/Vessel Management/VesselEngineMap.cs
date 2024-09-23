using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class VesselEngineMap : EntityTypeConfiguration<VesselEngine>
	{
		public VesselEngineMap()
		{
			// Primary Key
			this.HasKey(t => t.VesselEngineID);

			// Properties
			this.Property(t => t.EngineType)
				.HasMaxLength(4);

			this.Property(t => t.PropulsionType)
				.HasMaxLength(4);

			this.Property(t => t.RecordStatus)
				.IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("VesselEngine");
			this.Property(t => t.VesselEngineID).HasColumnName("VesselEngineID");
			this.Property(t => t.VesselID).HasColumnName("VesselID");
			this.Property(t => t.EnginePower).HasColumnName("EnginePower");
			this.Property(t => t.EngineType).HasColumnName("EngineType");
			this.Property(t => t.PropulsionType).HasColumnName("PropulsionType");
			this.Property(t => t.NoOfPropellers).HasColumnName("NoOfPropellers");
			this.Property(t => t.MaxSpeed).HasColumnName("MaxSpeed");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

			// Relationships
			this.HasOptional(t => t.SubCategory)
				.WithMany(t => t.VesselEngines)
				.HasForeignKey(d => d.EngineType);
			this.HasOptional(t => t.SubCategory1)
				.WithMany(t => t.VesselEngines1)
				.HasForeignKey(d => d.PropulsionType);
			this.HasRequired(t => t.User)
				.WithMany(t => t.VesselEngines)
				.HasForeignKey(d => d.CreatedBy);
			this.HasRequired(t => t.User1)
				.WithMany(t => t.VesselEngines1)
				.HasForeignKey(d => d.ModifiedBy);
			this.HasRequired(t => t.Vessel)
				.WithMany(t => t.VesselEngines)
				.HasForeignKey(d => d.VesselID);

		}
	}
}
