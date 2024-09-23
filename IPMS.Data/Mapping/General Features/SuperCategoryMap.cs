using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class SuperCategoryMap : EntityTypeConfiguration<SuperCategory>
	{
		public SuperCategoryMap()
		{
			// Primary Key
			this.HasKey(t => t.SupCatCode);

			// Properties
			this.Property(t => t.SupCatCode)
				.IsRequired()
				.HasMaxLength(4);

			this.Property(t => t.SupCatName)
				.IsRequired()
				.HasMaxLength(100);

			this.Property(t => t.RecordStatus)
				.IsFixedLength()
				.HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("SuperCategory");
			this.Property(t => t.SupCatCode).HasColumnName("SupCatCode");
			this.Property(t => t.SupCatName).HasColumnName("SupCatName");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
		}
	}
}
