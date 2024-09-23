using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class RoleMap : EntityTypeConfiguration<Role>
	{
		public RoleMap()
		{
			// Primary Key
			this.HasKey(t => t.RoleID);

			// Properties
			this.Property(t => t.RoleName)
				.IsRequired()
				.HasMaxLength(100);

			this.Property(t => t.RecordStatus)
				.IsFixedLength()
				.HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("Role");
			this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.RoleCode).HasColumnName("RoleCode");
			this.Property(t => t.RoleName).HasColumnName("RoleName");
            this.Property(t => t.RoleDescription).HasColumnName("RoleDescription");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

			// Relationships
			this.HasRequired(t => t.User)
				.WithMany(t => t.Roles)
				.HasForeignKey(d => d.CreatedBy);
			this.HasOptional(t => t.User1)
				.WithMany(t => t.Roles1)
				.HasForeignKey(d => d.ModifiedBy);

		}
	}
}
