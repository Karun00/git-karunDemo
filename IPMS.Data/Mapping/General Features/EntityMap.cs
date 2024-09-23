using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class EntityMap : EntityTypeConfiguration<Entity>
	{
		public EntityMap()
		{
			// Primary Key
			this.HasKey(t => t.EntityID);

			// Properties
            this.Property(t => t.EntityCode)
                .IsRequired()
                .HasMaxLength(50);
			
			this.Property(t => t.EntityName)
				.IsRequired()
				.HasMaxLength(100);

			this.Property(t => t.PageUrl)
				.IsRequired()
				.HasMaxLength(100);

			this.Property(t => t.RecordStatus)
				.IsFixedLength()
				.HasMaxLength(1);

            this.Property(t => t.ControllerName)
                .HasMaxLength(50);

            this.Property(t => t.Tokens)
                .HasMaxLength(1000);

            this.Property(t => t.HasWorkFlow)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.HasMenuItem)
                .IsFixedLength()
                .HasMaxLength(1);            

            this.Property(t => t.EntityCode)
                .HasMaxLength(50);

            this.Property(t => t.PendingTaskColumns)
                .HasMaxLength(1000);

			// Table & Column Mappings
			this.ToTable("Entity");
			this.Property(t => t.EntityID).HasColumnName("EntityID");
            this.Property(t => t.EntityCode).HasColumnName("EntityCode");
			this.Property(t => t.ModuleID).HasColumnName("ModuleID");
			this.Property(t => t.EntityName).HasColumnName("EntityName");
			this.Property(t => t.PageUrl).HasColumnName("PageUrl");
			this.Property(t => t.OrderNo).HasColumnName("OrderNo");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Tokens).HasColumnName("Tokens");
            this.Property(t => t.HasWorkFlow).HasColumnName("HasWorkFlow");
            this.Property(t => t.EntityCode).HasColumnName("EntityCode");
            this.Property(t => t.HasMenuItem).HasColumnName("HasMenuItem");
            this.Property(t => t.PendingTaskColumns).HasColumnName("PendingTaskColumns");
            this.Property(t => t.ControllerName).HasColumnName("ControllerName");

			// Relationships
			this.HasRequired(t => t.User)
				.WithMany(t => t.Entities)
				.HasForeignKey(d => d.CreatedBy);
			this.HasOptional(t => t.User1)
				.WithMany(t => t.Entities1)
				.HasForeignKey(d => d.ModifiedBy);
			this.HasRequired(t => t.Module)
				.WithMany(t => t.Entities)
				.HasForeignKey(d => d.ModuleID);

		}
	}
}
