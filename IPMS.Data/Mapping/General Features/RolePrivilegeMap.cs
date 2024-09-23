using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	
	public class RolePrivilegeMap : EntityTypeConfiguration<RolePrivilege>
	{
		public RolePrivilegeMap()
		{
			// Primary Key
			this.HasKey(t => new { t.RoleID, t.EntityID, t.SubCatCode });

			// Properties
			this.Property(t => t.RoleID)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			this.Property(t => t.EntityID)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			this.Property(t => t.SubCatCode)
				.IsRequired()
				.HasMaxLength(4);

			this.Property(t => t.RecordStatus)
				.IsFixedLength()
				.HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("RolePrivilege");
			this.Property(t => t.RoleID).HasColumnName("RoleID");
			this.Property(t => t.EntityID).HasColumnName("EntityID");
			this.Property(t => t.SubCatCode).HasColumnName("SubCatCode");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

			// Relationships
			this.HasRequired(t => t.EntityPrivilege)
				.WithMany(t => t.RolePrivileges)
				.HasForeignKey(d => new { d.EntityID, d.SubCatCode });
			this.HasRequired(t => t.Role)
				.WithMany(t => t.RolePrivileges)
				.HasForeignKey(d => d.RoleID);

		}
	}
}
