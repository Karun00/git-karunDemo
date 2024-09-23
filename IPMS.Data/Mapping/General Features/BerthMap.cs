using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class BerthMap : EntityTypeConfiguration<Berth>
	{
		public BerthMap()
		{
			// Primary Key
			this.HasKey(t => new { t.PortCode, t.QuayCode, t.BerthCode });

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

			this.Property(t => t.BerthName)
				.IsRequired()
				.HasMaxLength(15);

			this.Property(t => t.ShortName)
				.IsRequired()
				.HasMaxLength(10);

			this.Property(t => t.BerthType)
				.IsRequired()
				.HasMaxLength(4);

			this.Property(t => t.RecordStatus)
				.IsFixedLength()
				.HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("Berth");
			this.Property(t => t.PortCode).HasColumnName("PortCode");
			this.Property(t => t.QuayCode).HasColumnName("QuayCode");
			this.Property(t => t.BerthCode).HasColumnName("BerthCode");
			this.Property(t => t.BerthName).HasColumnName("BerthName");
			this.Property(t => t.ShortName).HasColumnName("ShortName");
			this.Property(t => t.BerthType).HasColumnName("BerthType");
			this.Property(t => t.FromMeter).HasColumnName("FromMeter");
			this.Property(t => t.ToMeter).HasColumnName("ToMeter");
			this.Property(t => t.Lengthm).HasColumnName("Lengthm");
			this.Property(t => t.Draftm).HasColumnName("Draftm");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.TidalDraft).HasColumnName("TidalDraft");

			// Relationships
			this.HasRequired(t => t.SubCategory)
				.WithMany(t => t.Berths)
				.HasForeignKey(d => d.BerthType);
			this.HasRequired(t => t.User)
				.WithMany(t => t.Berths)
				.HasForeignKey(d => d.CreatedBy);
			this.HasRequired(t => t.User1)
				.WithMany(t => t.Berths1)
				.HasForeignKey(d => d.ModifiedBy);
			this.HasRequired(t => t.Quay)
				.WithMany(t => t.Berths)
				.HasForeignKey(d => new { d.PortCode, d.QuayCode });

		}
	}
}
