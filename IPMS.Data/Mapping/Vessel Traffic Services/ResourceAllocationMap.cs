using IPMS.Domain.Models;
using Core.Repository;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;


namespace IPMS.Data.Mapping
{
    public class ResourceAllocationMap : EntityTypeConfiguration<ResourceAllocation>
    {
        public ResourceAllocationMap()
        {
            // Primary Key
            this.HasKey(t => t.ResourceAllocationID);

            // Properties
            this.Property(t => t.ServiceReferenceType)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.OperationType)
                .HasMaxLength(4);

            this.Property(t => t.ResourceType)
                .HasMaxLength(4);

            this.Property(t => t.TaskStatus)
                .HasMaxLength(4);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // -- Added by sandeep on 22-09-2014
            // Properties
            this.Property(t => t.AllocSlot)
                .HasMaxLength(20);
            // -- end

            this.Property(t => t.IsConfirm);

            // Table & Column Mappings
            this.ToTable("ResourceAllocation");
            this.Property(t => t.ResourceAllocationID).HasColumnName("ResourceAllocationID");
            this.Property(t => t.ServiceReferenceType).HasColumnName("ServiceReferenceType");
            this.Property(t => t.ServiceReferenceID).HasColumnName("ServiceReferenceID");
            this.Property(t => t.OperationType).HasColumnName("OperationType");
            this.Property(t => t.ResourceID).HasColumnName("ResourceID");
            this.Property(t => t.ResourceType).HasColumnName("ResourceType");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
            this.Property(t => t.TaskStatus).HasColumnName("TaskStatus");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AcknowledgeDate).HasColumnName("AcknowledgeDate");
            this.Property(t => t.Remarks).HasColumnName("Remarks");

            // -- Added by sandeep on 22-09-2014
            this.Property(t => t.AllocSlot).HasColumnName("AllocSlot");
            this.Property(t => t.CraftID).HasColumnName("CraftID");
            this.Property(t => t.AllocationDate).HasColumnName("AllocationDate");
            // -- end
            this.Property(t => t.IsConfirm).HasColumnName("IsConfirm");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.ResourceAllocations)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.ResourceAllocations1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasOptional(t => t.SubCategory)
                .WithMany(t => t.ResourceAllocations)
                .HasForeignKey(d => d.OperationType);
            this.HasRequired(t => t.User2)
                .WithMany(t => t.ResourceAllocations2)
                .HasForeignKey(d => d.ResourceID);
            this.HasOptional(t => t.SubCategory1)
                .WithMany(t => t.ResourceAllocations1)
                .HasForeignKey(d => d.ResourceType);
            this.HasRequired(t => t.SubCategory2)
                .WithMany(t => t.ResourceAllocations2)
                .HasForeignKey(d => d.ServiceReferenceType);
            this.HasOptional(t => t.SubCategory3)
                .WithMany(t => t.ResourceAllocations3)
                .HasForeignKey(d => d.TaskStatus);

            // -- Added by sandeep on 29-09-2014
            this.HasOptional(t => t.Craft)
               .WithMany(t => t.ResourceAllocations)
               .HasForeignKey(d => d.CraftID);
            // -- end

        }
    }
}
