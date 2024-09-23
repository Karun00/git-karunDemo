using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class CodeDefinitionMap : EntityTypeConfiguration<CodeDefinition>
	{
		public CodeDefinitionMap()
		{
			// Primary Key
			this.HasKey(t => t.CodeDefinitionID);

			// Properties
			this.Property(t => t.CodeName)
				.IsRequired()
                .HasMaxLength(10);

			this.Property(t => t.Description)
				.IsRequired()
                .HasMaxLength(100);

			this.Property(t => t.IsMonth)
				.IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

			this.Property(t => t.RecordStatus)
				.IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("CodeDefinition");
			this.Property(t => t.CodeDefinitionID).HasColumnName("CodeDefinitionID");
			this.Property(t => t.CodeName).HasColumnName("CodeName");
			this.Property(t => t.Description).HasColumnName("Description");
			this.Property(t => t.IsMonth).HasColumnName("IsMonth");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

			// Relationships
			this.HasRequired(t => t.User)
				.WithMany(t => t.CodeDefinitions)
				.HasForeignKey(d => d.CreatedBy);
			this.HasRequired(t => t.User1)
				.WithMany(t => t.CodeDefinitions1)
				.HasForeignKey(d => d.ModifiedBy);

		}
	}
}
