using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class MovementResourceAllocationMap : EntityTypeConfiguration<MovementResourceAllocation>
	{
		public MovementResourceAllocationMap()
		{
			// Primary Key
			this.HasKey(t => t.MovementResourceAllocationID);

			// Properties
			this.Property(t => t.ResourceType)
				.IsRequired()
                .HasMaxLength(4);

			this.Property(t => t.RecordStatus)
				.IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("MovementResourceAllocation");
			this.Property(t => t.MovementResourceAllocationID).HasColumnName("MovementResourceAllocationID");
			this.Property(t => t.ServiceRequestID).HasColumnName("ServiceRequestID");
			this.Property(t => t.ResourceType).HasColumnName("ResourceType");
			this.Property(t => t.ResourceID).HasColumnName("ResourceID");
			this.Property(t => t.MovementDateTime).HasColumnName("MovementDateTime");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

			// Relationships
			this.HasRequired(t => t.User)
				.WithMany(t => t.MovementResourceAllocations)
				.HasForeignKey(d => d.CreatedBy);
			this.HasRequired(t => t.User1)
				.WithMany(t => t.MovementResourceAllocations1)
				.HasForeignKey(d => d.ModifiedBy);
			this.HasRequired(t => t.SubCategory)
				.WithMany(t => t.MovementResourceAllocations)
				.HasForeignKey(d => d.ResourceType);
			this.HasRequired(t => t.ServiceRequest)
				.WithMany(t => t.MovementResourceAllocations)
				.HasForeignKey(d => d.ServiceRequestID);

		}
	}
}
