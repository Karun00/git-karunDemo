using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class BollardMap : EntityTypeConfiguration<Bollard>
	{
		public BollardMap()
		{
			// Primary Key
			this.HasKey(t => new { t.PortCode, t.QuayCode, t.BerthCode, t.BollardCode });

			// Properties
			this.Property(t => t.PortCode)
				.IsRequired()
				.HasMaxLength(2);

			this.Property(t => t.QuayCode)
				.IsRequired()
				.HasMaxLength(4);

			this.Property(t => t.BerthCode)
				.IsRequired()
				.HasMaxLength(4);

			this.Property(t => t.BollardCode)
				.IsRequired()
				.HasMaxLength(4);

			this.Property(t => t.BollardName)
				.IsRequired()
				.HasMaxLength(15);

			this.Property(t => t.ShortName)
				.IsRequired()
				.HasMaxLength(10);

			this.Property(t => t.Continuous)
				.IsFixedLength()
				.HasMaxLength(1);

            this.Property(t => t.Coordinates)
             .HasMaxLength(100);

			this.Property(t => t.Description)
				.HasMaxLength(100);

			this.Property(t => t.RecordStatus)
				.IsFixedLength()
				.HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("Bollard");
			this.Property(t => t.PortCode).HasColumnName("PortCode");
			this.Property(t => t.QuayCode).HasColumnName("QuayCode");
			this.Property(t => t.BerthCode).HasColumnName("BerthCode");
			this.Property(t => t.BollardCode).HasColumnName("BollardCode");
			this.Property(t => t.BollardName).HasColumnName("BollardName");
			this.Property(t => t.ShortName).HasColumnName("ShortName");
			this.Property(t => t.FromMeter).HasColumnName("FromMeter");
			this.Property(t => t.ToMeter).HasColumnName("ToMeter");
			this.Property(t => t.Continuous).HasColumnName("Continuous");
            this.Property(t => t.Coordinates).HasColumnName("Coordinates");
             this.Property(t => t.OffsetCoordinates).HasColumnName("OffsetCoordinates");
             this.Property(t => t.MidCoordinates).HasColumnName("MidCoordinates");
            this.Property(t => t.Description).HasColumnName("Description");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

			// Relationships
			this.HasRequired(t => t.Berth)
				.WithMany(t => t.Bollards)
				.HasForeignKey(d => new { d.PortCode, d.QuayCode, d.BerthCode });
			this.HasRequired(t => t.User)
				.WithMany(t => t.Bollards)
				.HasForeignKey(d => d.CreatedBy);
			this.HasRequired(t => t.User1)
				.WithMany(t => t.Bollards1)
				.HasForeignKey(d => d.ModifiedBy);

		}
	}
}
