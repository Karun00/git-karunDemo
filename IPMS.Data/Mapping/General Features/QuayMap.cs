using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class QuayMap : EntityTypeConfiguration<Quay>
	{
		public QuayMap()
		{
			// Primary Key
			this.HasKey(t => new { t.PortCode, t.QuayCode });

			// Properties
			this.Property(t => t.PortCode)
				.IsRequired()
				.HasMaxLength(2);

			this.Property(t => t.QuayCode)
				.IsRequired()
				.HasMaxLength(4);

			this.Property(t => t.ShortName)
				.IsRequired()
				.HasMaxLength(10);

			this.Property(t => t.QuayName)
				.IsRequired()
				.HasMaxLength(20);

			this.Property(t => t.Description)
				.HasMaxLength(100);

			this.Property(t => t.RecordStatus)
				.IsFixedLength()
				.HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("Quay");
			this.Property(t => t.PortCode).HasColumnName("PortCode");
			this.Property(t => t.QuayCode).HasColumnName("QuayCode");
			this.Property(t => t.ShortName).HasColumnName("ShortName");
			this.Property(t => t.QuayName).HasColumnName("QuayName");
			this.Property(t => t.QuayLength).HasColumnName("QuayLength");
			this.Property(t => t.Description).HasColumnName("Description");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

			// Relationships
			this.HasRequired(t => t.Port)
				.WithMany(t => t.Quays)
				.HasForeignKey(d => d.PortCode);
			this.HasRequired(t => t.User)
				.WithMany(t => t.Quays)
				.HasForeignKey(d => d.CreatedBy);
			this.HasRequired(t => t.User1)
				.WithMany(t => t.Quays1)
				.HasForeignKey(d => d.ModifiedBy);

		}
	}
}
