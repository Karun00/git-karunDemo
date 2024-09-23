using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class PortMap : EntityTypeConfiguration<Port>
	{
		public PortMap()
		{
			// Primary Key
			this.HasKey(t => t.PortCode);

			// Properties
			this.Property(t => t.PortCode)
				.IsRequired()
				.HasMaxLength(2);

			this.Property(t => t.PortName)
				.IsRequired()
				.HasMaxLength(20);

			this.Property(t => t.InternationalCharacter)
				.HasMaxLength(10);

			this.Property(t => t.GeographicLocation)
				.HasMaxLength(15);

			this.Property(t => t.Email)
				.IsRequired()
				.HasMaxLength(50);

			this.Property(t => t.Website)
				.HasMaxLength(50);

			this.Property(t => t.Description)
				.HasMaxLength(100);

			this.Property(t => t.RecordStatus)
				.IsFixedLength()
				.HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("Port");
			this.Property(t => t.PortCode).HasColumnName("PortCode");
			this.Property(t => t.PortName).HasColumnName("PortName");
			this.Property(t => t.InternationalCharacter).HasColumnName("InternationalCharacter");
			this.Property(t => t.GeographicLocation).HasColumnName("GeographicLocation");
			this.Property(t => t.ContactNo).HasColumnName("ContactNo");
			this.Property(t => t.Email).HasColumnName("Email");
			this.Property(t => t.Fax).HasColumnName("Fax");
			this.Property(t => t.Website).HasColumnName("Website");
			this.Property(t => t.Description).HasColumnName("Description");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

			// Relationships
			this.HasRequired(t => t.CreatedUser)
				.WithMany(t => t.Ports)
				.HasForeignKey(d => d.CreatedBy);
			this.HasOptional(t => t.ModifiedUser)
				.WithMany(t => t.Ports1)
				.HasForeignKey(d => d.ModifiedBy);

		}
	}
}
