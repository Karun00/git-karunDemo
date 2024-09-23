using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class AuthorizedContactPersonMap : EntityTypeConfiguration<AuthorizedContactPerson>
	{
		public AuthorizedContactPersonMap()
		{
			// Primary Key
			this.HasKey(t => t.AuthorizedContactPersonID);

			// Properties
			this.Property(t => t.AuthorizedContactPersonType)
				.IsRequired()
				.HasMaxLength(4);

			this.Property(t => t.FirstName)
				.IsRequired()
				.HasMaxLength(100);

			this.Property(t => t.SurName)
				.IsRequired()
				.HasMaxLength(100);

			this.Property(t => t.IdentityNo)
				.IsRequired()
				.HasMaxLength(15);

			this.Property(t => t.Designation)
				.IsRequired()
				.HasMaxLength(15);

			this.Property(t => t.EmailID)
				.IsRequired()
				.HasMaxLength(50);

			this.Property(t => t.RecordStatus)
				.IsFixedLength()
				.HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("AuthorizedContactPerson");
			this.Property(t => t.AuthorizedContactPersonID).HasColumnName("AuthorizedContactPersonID");
			this.Property(t => t.AuthorizedContactPersonType).HasColumnName("AuthorizedContactPersonType");
			this.Property(t => t.FirstName).HasColumnName("FirstName");
			this.Property(t => t.SurName).HasColumnName("SurName");
			this.Property(t => t.IdentityNo).HasColumnName("IdentityNo");
			this.Property(t => t.Designation).HasColumnName("Designation");
			this.Property(t => t.CellularNo).HasColumnName("CellularNo");
			this.Property(t => t.EmailID).HasColumnName("EmailID");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

			// Relationships
			this.HasRequired(t => t.SubCategory)
				.WithMany(t => t.AuthorizedContactPersons)
				.HasForeignKey(d => d.AuthorizedContactPersonType);
			this.HasRequired(t => t.User)
				.WithMany(t => t.AuthorizedContactPersons)
				.HasForeignKey(d => d.CreatedBy);
			this.HasOptional(t => t.User1)
				.WithMany(t => t.AuthorizedContactPersons1)
				.HasForeignKey(d => d.ModifiedBy);

		}
	}
}
