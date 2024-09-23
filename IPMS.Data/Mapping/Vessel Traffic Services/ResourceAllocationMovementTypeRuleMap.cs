using IPMS.Domain.Models;
using Core.Repository;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;


namespace IPMS.Data.Mapping
{
    public class ResourceAllocationMovementTypeRuleMap : EntityTypeConfiguration<ResourceAllocationMovementTypeRule>
    {
        public ResourceAllocationMovementTypeRuleMap()
        {
            // Primary Key
            this.HasKey(t => t.ResourceAllocationMovementTypeRuleID);

            // Properties
            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.MovementType)
                .IsRequired()
                .HasMaxLength(4);

            // Table & Column Mappings
            this.ToTable("ResourceAllocationMovementTypeRule");
            this.Property(t => t.ResourceAllocationMovementTypeRuleID).HasColumnName("ResourceAllocationMovementTypeRuleID");
            this.Property(t => t.ResourceAllocationConfigRuleID).HasColumnName("ResourceAllocationConfigRuleID");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.MovementType).HasColumnName("MovementType");
            this.Property(t => t.ServiceTypeID).HasColumnName("ServiceTypeID");

            // Relationships
            this.HasRequired(t => t.Port)
                .WithMany(t => t.ResourceAllocationMovementTypeRules)
                .HasForeignKey(d => d.PortCode);
            this.HasRequired(t => t.ResourceAllocationConfigRule)
                .WithMany(t => t.ResourceAllocationMovementTypeRules)
                .HasForeignKey(d => d.ResourceAllocationConfigRuleID);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.ResourceAllocationMovementTypeRules)
                .HasForeignKey(d => d.MovementType);
            this.HasRequired(t => t.ServiceType)
                .WithMany(t => t.ResourceAllocationMovementTypeRules)
                .HasForeignKey(d => d.ServiceTypeID);

        }
    }
}
