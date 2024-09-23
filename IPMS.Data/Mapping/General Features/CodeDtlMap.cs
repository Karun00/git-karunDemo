using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class CodeDtlMap : EntityTypeConfiguration<CodeDtl>
	{
		public CodeDtlMap()
		{
			// Primary Key
			this.HasKey(t => t.CodeDtlID);

			// Properties
			this.Property(t => t.RecordStatus)
				.IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("CodeDtl");
			this.Property(t => t.CodeDtlID).HasColumnName("CodeDtlID");
			this.Property(t => t.CodeID).HasColumnName("CodeID");
			this.Property(t => t.StartValue).HasColumnName("StartValue");
			this.Property(t => t.CurValue).HasColumnName("CurValue");
			this.Property(t => t.CodeDtlMonth).HasColumnName("CodeDtlMonth");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

			// Relationships
			this.HasRequired(t => t.Code)
				.WithMany(t => t.CodeDtls)
				.HasForeignKey(d => d.CodeID);
			this.HasRequired(t => t.User)
				.WithMany(t => t.CodeDtls)
				.HasForeignKey(d => d.CreatedBy);
			this.HasRequired(t => t.User1)
				.WithMany(t => t.CodeDtls1)
				.HasForeignKey(d => d.ModifiedBy);

		}
	}
}
